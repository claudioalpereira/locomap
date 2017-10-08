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
                using (var command = new SqlCommand(@"SELECT [PK], [LastUpdate], [Loco], [Longitude], [Latitude], [Altitude], [Mileage], [EnergReactCons], [EnergReactDev], [EnergActCons], [EnergActDev], [Uptime], [UnetInst] FROM [dbo].[LocoStatus]", connection))
                {

                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new LocoStatus
                        {
                            PK = (int)reader["PK"],
                            LastUpdate = Convert.ToDateTime(reader["LastUpdate"]),
                            Loco = (string)reader["Loco"],
                            Longitude = (double)(decimal)reader["Longitude"],
                            Latitude = (double)(decimal)reader["Latitude"],
                            Altitude = (double)(decimal)reader["Altitude"],
                            Mileage = (double)(decimal)reader["Mileage"],
                            EnergReactCons = (double)(decimal)reader["EnergReactCons"],
                            EnergReactDev = (double)(decimal)reader["EnergReactDev"],
                            EnergActCons = (double)(decimal)reader["EnergActCons"],
                            EnergActDev = (double)(decimal)reader["EnergActDev"],
                            Uptime = (double)(decimal)reader["Uptime"],
                            UnetInst = (int)reader["UnetInst"]
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
                MessagesHub.SendMessages();
            }
        }
    }
}