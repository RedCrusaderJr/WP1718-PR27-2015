using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;

namespace TaxiApp.Models.NonDbModels.UsersControllerModels
{
    public class GeneralUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EGender Gender { get; set; }
        public string JMBG { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<string> TaxiDrivesIDs { get; set; }
    }
}