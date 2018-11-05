using System.Collections.Generic;
using TaxiApp.Models;

namespace TaxiApp.Common
{
    public interface IUser
    {
        string Username { get; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        EGender Gender { get; set; }
        string JMBG { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        ERole Role { get; }
        List<TaxiDrive> TaxiDrives { get; set; }
    }
}