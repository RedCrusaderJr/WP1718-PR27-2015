using TaxiApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaxiApp.Models
{
    public class Customer : IUser
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

        public Customer()
        {
            Role = ERole.CUSTOMER;
            TaxiDrives = new List<TaxiDrive>();
        }

        public Customer(string username, string password)
        {
            Role = ERole.CUSTOMER;
            TaxiDrives = new List<TaxiDrive>();

            Username = username;
            Password = password;
        }

        public Customer(Customer c)
        {
            Role = ERole.CUSTOMER;
            TaxiDrives = new List<TaxiDrive>();

            Username = c.Username;
            Password = c.Password;
            FirstName = c.FirstName;
            LastName = c.LastName;
            Gender = c.Gender;
            JMBG = c.JMBG;
            Phone = c.Phone;
            Email = c.Email;

            c.TaxiDrives.ForEach(td => this.TaxiDrives.Add(td));
        }
    }
}