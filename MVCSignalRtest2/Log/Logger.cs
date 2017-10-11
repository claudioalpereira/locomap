using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCSignalRtest2.Log
{
    public class Logger
    {
        private static object mutex = new object();
        private static DateTime _lastFachina;

        private static string GetPath()
        {
            string path = ConfigurationManager.AppSettings["logDir"] ?? "C:\\Temp";
         
            if (!path.EndsWith("\\")) path += "\\";
            return path;
        }

        public static void Log(string msg)
        {
            Console.WriteLine(msg);

            lock (mutex)
            {
                var dt = DateTime.Now;

                if (_lastFachina == null || _lastFachina < dt.AddMonths(-1))
                {
                    Fachina();
                    _lastFachina = dt;
                }
                System.IO.StreamWriter sw = System.IO.File.AppendText(
                        string.Format("{0}log_{1}-{2:00}.txt", GetPath(), dt.Year, dt.Month));
                try
                {
                    sw.WriteLine(string.Format("{0:G}: {1}.", dt, msg));
                }
                finally
                {
                    sw.Close();
                }
            }
        }


        public static void Error(string msg)
        {
            Log("ERROR: " + msg);
        }


        public static void Exception(Exception ex)
        {
            Log(string.Format("EXCEPTION: {0}\n{1}", ex.Message, ex.StackTrace.ToString()));
        }

        private static void Fachina()
        {
            foreach (string file in Directory.GetFiles(GetPath()))
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Name.StartsWith("log_") && fi.Extension.Equals(".txt") && fi.CreationTime < DateTime.Now.AddMonths(-6))
                {
                    fi.Delete();
                }
            }
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
