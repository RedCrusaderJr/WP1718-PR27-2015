using TaxiApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApp.Models
{
    public class Admin : IUser
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
        #endregion

        public Admin()
        {
            Role = ERole.ADMIN;
            TaxiDrives = new List<TaxiDrive>();
        }

        public Admin(string username, string password)
        {
            Role = ERole.ADMIN;
            TaxiDrives = new List<TaxiDrive>();

            Username = username;
            Password = password;
        }

        public Admin(Admin a)
        {
            Role = ERole.ADMIN;
            TaxiDrives = new List<TaxiDrive>();

            Username = a.Username;
            Password = a.Password;
            FirstName = a.FirstName;
            LastName = a.LastName;
            Gender = a.Gender;
            JMBG = a.JMBG;
            Phone = a.Phone;
            Email = a.Email;

            a.TaxiDrives.ForEach(td => this.TaxiDrives.Add(td));
        }
    }
}