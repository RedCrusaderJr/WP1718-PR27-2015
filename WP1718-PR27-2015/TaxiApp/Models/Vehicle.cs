using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaxiApp.Common;

namespace TaxiApp.Models
{
    public class Vehicle
    {
        #region Properties
        [Key]
        public string VehicleID { get; private set; }

        public string LicencePlateNo { get; set; }
        public string ProductionYear { get; set; }
        public EVehicleType VehicleType { get; set; }

        public Driver VehicleDriver { get; set; } 
        #endregion

        public Vehicle() { }

        public Vehicle(string id)
        {
            VehicleID = id;
        }

        public Vehicle(Vehicle v)
        {
            VehicleID = v.VehicleID;

            LicencePlateNo = v.LicencePlateNo;
            ProductionYear = v.ProductionYear;
            VehicleType = v.VehicleType;

            VehicleDriver = v.VehicleDriver;
        }
    }
}