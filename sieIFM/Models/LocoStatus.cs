using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSignalRtest2.Models
{
    public class LocoStatus
    {
        public int PK { get; set; }
        public DateTime LastGPSUpdate { get; set; }
        public DateTime LastMeterUpdate { get; set; }
        public string Loco { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public double Mileage { get; set; }
        public double Hours { get; set; }
        public double EnergReactCons { get; set; }
        public double EnergReactDev { get; set; }
        public double EnergActCons { get; set; }
        public double EnergActDev { get; set; }
        public double Uptime { get; set; }
        public double UnetInst { get; set; }
    }
}