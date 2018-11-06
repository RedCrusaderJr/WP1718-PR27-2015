using TaxiApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApp.Models
{
    public class Driver : IUser
    {
        #region Properties
        [Key]
        public string Username { get; private set; }
        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EGender Gender { get; set; }
        public string JMBG { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public ERole Role { get; private set; }

        public List<TaxiDrive> TaxiDrives { get; set; }

        public Location DriversLocation { get; set; }
        public Vehicle DriversVehicle { get; set; }
        #endregion

        public Driver()
        {
            Role = ERole.DRIVER;
            TaxiDrives = new List<TaxiDrive>();
        }

        public Driver(string username, string password)
        {
            Role = ERole.DRIVER;
            TaxiDrives = new List<TaxiDrive>();

            Username = username;
            Password = password;
        }

        public Driver(Driver d)
        {
            Role = ERole.DRIVER;
            TaxiDrives = new List<TaxiDrive>();

            Username = d.Username;
            Password = d.Password;
            FirstName = d.FirstName;
            LastName = d.LastName;
            Gender = d.Gender;
            JMBG = d.JMBG;
            Phone = d.Phone;
            Email = d.Email;

            d.TaxiDrives.ForEach(td => this.TaxiDrives.Add(td));

            DriversLocation = d.DriversLocation;
            DriversVehicle = d.DriversVehicle;
        }
    }
}