using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiApp.Models
{
    public class Location
    {
        #region Fields
        private string _xCoordinate = string.Empty;
        private string _yCoordinate = string.Empty;
        private string _locationID = string.Empty;
        #endregion

        #region Properties
        public String XCoordinate
        {
            get
            {
                return _xCoordinate;
            }
            private set
            {
                _xCoordinate = value;
                LocationID = $"{XCoordinate}|{YCoordinate}";
            }
        }
        public String YCoordinate
        {
            get
            {
                return _yCoordinate;
            }
            private set
            {
                _yCoordinate = value;
                LocationID = $"{XCoordinate}|{YCoordinate}";
            }
        }
        [Key]
        public String LocationID
        {
            get
            {
                return _locationID;
            }
            private set
            {
                _locationID = value;
            }
        }

        public String StreetName { get; set; }
        public String StreetNumber { get; set; }
        public String City { get; set; }
        public String ZipCode { get; set; }
        #endregion

        public Location() { }

        public Location(string x, string y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }

        public Location(Location l)
        {
            XCoordinate = l.XCoordinate;
            YCoordinate = l.YCoordinate;
            StreetName = l.StreetName;
            StreetNumber = l.StreetNumber;
            City = l.City;
            ZipCode = l.ZipCode;
        }
    }
}