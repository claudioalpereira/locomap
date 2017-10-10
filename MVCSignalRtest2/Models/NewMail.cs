using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace MVCSignalRtest2.Models
{
    public class Email : Postal.Email
    {
        public string To { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}