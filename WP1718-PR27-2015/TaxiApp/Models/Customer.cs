using TaxiApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiApp.Models
{
    public class Customer : IUser
    {
        public string Username { get; private set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EGender Gender { get; set; }

        public string JMBG { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ERole Role { get; private set; }

        public List<TaxiDrive> TaxiDrives { get; set; }
    }
}