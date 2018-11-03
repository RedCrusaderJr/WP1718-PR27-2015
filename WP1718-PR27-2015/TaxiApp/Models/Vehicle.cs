using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;

namespace TaxiApp.Models
{
    public class Vehicle
    {
        public string VehicleID { get; private set; }

        public string LicencePlateNo { get; set; }
        public string ProductionYear { get; set; }
        public EVehicleType VehicleType { get; set; }

        public Driver VehicleDriver { get; set; }
    }
}