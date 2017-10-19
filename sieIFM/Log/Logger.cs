using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCSignalRtest2.Log
{
    public class Logger
    {
        private static object mutex = new object();
        private static DateTime _lastFachina = DateTime.MinValue;

        private static string GetPath()
        {
            string path = "D:\\CL\\";//ConfigurationManager.AppSettings["log_logDir"] ?? "C:\\Temp";
         
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public static void Log(string msg)
        {
            lock (mutex)
            {
                try
                {
                    var dt = DateTime.Now;

                    var file = new FileInfo(string.Format("{0}log_{1}-{2:00}.txt", GetPath(), dt.Year, dt.Month));
                    file.Directory.Create();

                    File.AppendAllText(file.FullName, Environment.NewLine + msg);

                }
                catch (Exception)
                {
                    // DebaixoDoTapetator Pattern
                }            }
        }


        public static void Error(string msg)
        {
            Log("ERROR: " + msg);
        }


        public static void Log(Exception ex)
        {
            Log(string.Format("EXCEPTION: {0}", ex.ToString()));
        }

        private static void Fachina()
        {
            var now = DateTime.Now;

            if (_lastFachina > now.AddMonths(-1))
            {
                return;
            }

            var str = ConfigurationManager.AppSettings["log_months-to-keep-logs"];
            int months = int.TryParse(str, out months) ? months : 6;


            foreach (string file in Directory.GetFiles(GetPath()))
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Name.StartsWith("log_") && fi.Extension.Equals(".txt") && fi.CreationTime < now.AddMonths(-months))
                {
                    fi.Delete();
                }
            }

            _lastFachina = now;
        }

        #region Overload methods
        public static void Log(string text, object arg0)
        {
            Log(string.Format(text, arg0));
        }

        public static void Log(string text, object arg0, object arg1)
        {
            Log(string.Format(text, arg0, arg1));
        }

        public static void Log(string text, object arg0, object arg1, object arg2)
        {
            Log(string.Format(text, arg0, arg1, arg2));
        }
      
        public static void Error(string text, object arg0)
        {
            Error(string.Format(text, arg0));
        }

        public static void Error(string text, object arg0, object arg1)
        {
            Error(string.Format(text, arg0, arg1));
        }

        public static void Error(string text, object arg0, object arg1, object arg2)
        {
            Error(string.Format(text, arg0, arg1, arg2));
        }
        #endregion
    }

}
