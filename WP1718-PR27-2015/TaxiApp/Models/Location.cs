using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApp.Models
{
    public class Location
    {
        public String XCoordinate { get; set; }
        public String YCoordinate { get; set; }
        public String LocationID { get; private set; }

        public String StreetName { get; set; }
        public String StreetNumbe { get; set; }
        public String City { get; set; }
        public String ZipCode { get; set; }
    }
}