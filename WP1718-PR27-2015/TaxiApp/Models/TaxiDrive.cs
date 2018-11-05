using TaxiApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Input;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiApp.Models
{
    public class TaxiDrive
    {
        public string TaxiDriveID { get; private set; }

        public EVehicleType VehicleType { get; set; }
        public EDriveStatus DriveStatus { get; set; }
        public int Amount { get; set; }

        public Driver TaxiDriveDriver { get; set; }
        public Customer TaxiDriveCustomer { get; set; }
        public Admin TaxiDriveDispatcher { get; set; }

        public Comment TaxiDriveComment { get; set; }

        public Location TaxiDriveStartingLocation { get; set; }
        public Location TaxiDriveDestination { get; set; }
    }
}