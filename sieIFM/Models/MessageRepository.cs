using MVCSignalRtest2.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCSignalRtest2.Models
{
    public class MessagesRepository
    {
        private static object mutex = new object();
        private static SqlDependency dependency;

        static readonly string _connString =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static IEnumerable<LocoStatus> GetAllMessages(bool registerDependency = false)
        {
            var messages = new List<LocoStatus>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [PK], [LastGPSUpdate], [LastMeterUpdate], [Loco], [Longitude], [Latitude], [Altitude], [Mileage], [EnergReactCons], [EnergReactDev], [EnergActCons], [EnergActDev], [Uptime], [UnetInst] FROM [dbo].[LocoStatus]", connection))
                {
                    command.Notification = null;


                    lock(mutex)
                    {
                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        }
                    }

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int pk;
                        DateTime lastGPSUpdate;
                        DateTime lastMeterUpdate;
                        string loco;
                        double longitude;
                        double latitude;
                        double altitude;
                        double mileage;
                        double energReactCons;
                        double energReactDev;
                        double energActCons;
                        double energActDev;
                        double uptime;
                        int unetInst;

                        try
                        {
                            pk = (int)reader["PK"];
                            lastGPSUpdate = Convert.ToDateTime(reader["LastGPSUpdate"]);
                            lastMeterUpdate = Convert.ToDateTime(reader["LastMeterUpdate"]);
                            loco = (string)reader["Loco"];
                            longitude = (double)(decimal)reader["Longitude"];
                            latitude = (double)(decimal)reader["Latitude"];
                            altitude = (double)(decimal)reader["Altitude"];
                            mileage = (double)(decimal)reader["Mileage"];
                            energReactCons = (double)(decimal)reader["EnergReactCons"];
                            energReactDev = (double)(decimal)reader["EnergReactDev"];
                            energActCons = (double)(decimal)reader["EnergActCons"];
                            energActDev = (double)(decimal)reader["EnergActDev"];
                            uptime = (double)(decimal)reader["Uptime"];
                            unetInst = (int)reader["UnetInst"];

                        }
                        catch (Exception e)
                        {
                            // Escondator pattern
                            Log.Logger.Log(e);
                            continue;
                        }
                        messages.Add(item: new LocoStatus
                        {
                            PK = pk,
                            LastGPSUpdate = lastGPSUpdate,
                            LastMeterUpdate = lastMeterUpdate,
                            Loco = loco,
                            Longitude = (double)longitude,
                            Latitude = (double)latitude,
                            Altitude = (double)altitude,
                            Mileage = (double)mileage,
                            EnergReactCons = (double)energReactCons,
                            EnergReactDev = (double)energReactDev,
                            EnergActCons = (double)energActCons,
                            EnergActDev = (double)energActDev,
                            Uptime = (double)uptime,
                            UnetInst = (double)unetInst
                        });
                    }
                }
            }
            return messages;
        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                lock (mutex)
                {
                    dependency = null;
                }
                var locos = GetAllMessages();
                MessagesHub.SendMessages(locos);
            }
        }
    }
}
/*********************************************************************************************** 
 public class MessagesRepository
{
    static readonly string _connString =
    ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    public static IEnumerable<LocoStatus> GetAllMessages()
    {
        var messages = new List<LocoStatus>();
        using (var connection = new SqlConnection(_connString))
        {
            connection.Open();
            //using (var command = new SqlCommand(@"SELECT [MessageID], 
            //[Message], [EmptyMessage], [Date] FROM [dbo].[Messages]", connection))
            //{
            using (var command = new SqlCommand(@"SELECT [PK], [LastGPSUpdate], [LastMeterUpdate], [Loco], [Longitude], [Latitude], [Altitude], [Mileage], [EnergReactCons], [EnergReactDev], [EnergActCons], [EnergActDev], [Uptime], [UnetInst] FROM [dbo].[LocoStatus]", connection))
            {

                command.Notification = null;

                //var dependency = new SqlDependency(command);
                //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int pk;
                    DateTime lastGPSUpdate;
                    DateTime lastMeterUpdate;
                    string loco;
                    double longitude;
                    double latitude;
                    double altitude;
                    double mileage;
                    double energReactCons;
                    double energReactDev;
                    double energActCons;
                    double energActDev;
                    double uptime;
                    int unetInst;

                    try
                    {
                        pk = (int)reader["PK"];
                        lastGPSUpdate = Convert.ToDateTime(reader["LastGPSUpdate"]);
                        lastMeterUpdate = Convert.ToDateTime(reader["LastMeterUpdate"]);
                        loco = (string)reader["Loco"];
                        longitude = (double)(decimal)reader["Longitude"];
                        latitude = (double)(decimal)reader["Latitude"];
                        altitude = (double)(decimal)reader["Altitude"];
                        mileage = (double)(decimal)reader["Mileage"];
                        energReactCons = (double)(decimal)reader["EnergReactCons"];
                        energReactDev = (double)(decimal)reader["EnergReactDev"];
                        energActCons = (double)(decimal)reader["EnergActCons"];
                        energActDev = (double)(decimal)reader["EnergActDev"];
                        uptime = (double)(decimal)reader["Uptime"];
                        unetInst = (int)reader["UnetInst"];

                    }
                    catch (Exception e)
                    {
                        // Escondator pattern
                        Log.Logger.Log(e);
                        continue;
                    }
                    messages.Add(item: new LocoStatus
                    {
                        PK = pk,
                        LastGPSUpdate = lastGPSUpdate,
                        LastMeterUpdate = lastMeterUpdate,
                        Loco = loco,
                        Longitude = (double)longitude,
                        Latitude = (double)latitude,
                        Altitude = (double)altitude,
                        Mileage = (double)mileage,
                        EnergReactCons = (double)energReactCons,
                        EnergReactDev = (double)energReactDev,
                        EnergActCons = (double)energActCons,
                        EnergActDev = (double)energActDev,
                        Uptime = (double)uptime,
                        UnetInst = (double)unetInst
                    });
}
            }
        }
        return messages;
    }

    private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
{
if (e.Type == SqlNotificationType.Change)
{
    var locos = GetAllMessages();
    MessagesHub.SendMessages(locos);
}
}
}
*/
