using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Configuration;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace MVCSignalRtest2.Hubs
{
    [HubName("messagesHub")]
    public class MessagesHub : Hub
    {
        private static string conString =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages<T>(IEnumerable<T> list)
        {
            var json = JsonConvert.SerializeObject(list);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages(json);
        }
    }
}