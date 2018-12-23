using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TaxiApp.Common;
using TaxiApp.Database_Management.Access;
using TaxiApp.Models;
using TaxiApp.Models.NonDbModels;
using TaxiApp.Models.NonDbModels.UsersControllerModels;

namespace TaxiApp.Controllers
{
    //[RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        public List<string> LoggedUsers
        {
            get
            {
                return HttpContext.Current.Application["Logged"] as List<string>;
            }
        }

        public AdminDbAccess DbAdmin { get { return AdminDbAccess.Instance; } }
        public DriverDbAccess DbDriver { get { return DriverDbAccess.Instance; } }
        public CustomerDbAccess DbCustomer { get { return CustomerDbAccess.Instance; } }
        public CommentDbAccess DbComment { get { return CommentDbAccess.Instance; } }
        public TaxiDriveDbAccess DbTaxiDrive { get { return TaxiDriveDbAccess.Instance; } }
        public VehicleDbAccess DbVehicle { get { return VehicleDbAccess.Instance; } }
        public LocationDbAccess DbLocation { get { return LocationDbAccess.Instance; } }

        #region GET
        [HttpGet]
        [Route("api/users/get")]
        //[Route("get")]
        [ResponseType(typeof(IEnumerable<IUser>))]
        public IHttpActionResult GetUsers([FromUri]string senderID)
        {
            List<IUser> result = new List<IUser>();

            if(!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            //other rights?

            try
            {
                (DbAdmin.GetAll()).ToList().ForEach(a => result.Add(a));
                (DbCustomer.GetAll()).ToList().ForEach(c => result.Add(c));
                (DbDriver.GetAll()).ToList().ForEach(d => result.Add(d));
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'GetUsers()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/users/getNonDriver")]
        //[Route("getNonDriver")]
        [ResponseType(typeof(IUser))]
        public IHttpActionResult GetNonDriver([FromUri]string senderID, [FromUri]string userIdToGet)
        {
            IUser result = null;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            try
            {
                if(DbAdmin.Exists(userIdToGet))
                {
                    if (!DbAdmin.Exists(senderID))
                    {
                        return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
                    }

                    result = DbAdmin.GetSingleEntityByKey(userIdToGet);
                }
                else if (DbCustomer.Exists(userIdToGet))
                {
                    if (!DbAdmin.Exists(senderID) || senderID != userIdToGet)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user whose information are requested.");
                    }

                    result = DbAdmin.GetSingleEntityByKey(userIdToGet);
                }
            }
            catch(Exception e)
            {
                Trace.Write($"Error on 'GetNonDriver()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }


            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/users/getDriver")]
        //[Route("getDriver")]
        [ResponseType(typeof(Driver))]
        public IHttpActionResult GetDriver([FromUri]string senderID, [FromUri]string userIdToGet)
        {
            Driver result = null;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            try
            {
                if (DbDriver.Exists(userIdToGet))
                {
                    if (!DbAdmin.Exists(senderID) || senderID != userIdToGet)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user whose information are requested.");
                    }

                    result = DbDriver.GetSingleEntityByKey(userIdToGet);
                }
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'GetDriver()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("api/users/login")]
        //[Route("login")]
        [ResponseType(typeof(IUser))]
        public IHttpActionResult Login([FromBody]LoginModel loginModel)
        {
            if (LoggedUsers.Contains(loginModel.Username))
            {
                return Content(HttpStatusCode.Conflict, $"User '{loginModel.Username}' already logged in.");
            }

            IUser result = null;

            try
            {
                if (DbAdmin.Exists(loginModel.Username))
                {
                    result = DbAdmin.GetSingleEntityByKey(loginModel.Username);
                }
                else if (DbDriver.Exists(loginModel.Username))
                {
                    result = DbDriver.GetSingleEntityByKey(loginModel.Username);
                }
                else if (DbCustomer.Exists(loginModel.Username))
                {
                    result = DbCustomer.GetSingleEntityByKey(loginModel.Username);
                }
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'Login()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }

            if (result == null)
            {
                return NotFound();
            }

            if(result.Password.Equals(loginModel.Password))
            {
                LoggedUsers.Add(loginModel.Username);
                return Ok(result);
            }
            else
            {
                return BadRequest($"Entered password did not match the required one for user '{loginModel.Username}'.");
            }

        }

        [HttpPost]
        [Route("api/users/logout")]
        //[Route("logout")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Logout([FromBody]LogoutModel logoutModel)
        {
            if (!LoggedUsers.Contains(logoutModel.Username))
            {
                return BadRequest($"User '{logoutModel.Username}' was not logged in.");
            }
            else
            {
                LoggedUsers.Remove(logoutModel.Username);
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/users/postCustomer")]
        //[Route("postCustomer")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer([FromUri]string senderID, [FromBody]GeneralUserModel userModel)
        {
            Customer customer = new Customer(userModel.Username, userModel.Password)
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Gender = userModel.Gender,
                JMBG = userModel.JMBG,
                Phone = userModel.Phone,
                Email = userModel.Email,
                
            };
            userModel.TaxiDrivesIDs.ForEach(td => customer.TaxiDrives.Add(DbTaxiDrive.GetSingleEntityByKey(td)));

            //Customer ne pravi sam svoj nalog
            if (senderID != customer.Username)
            {
                if (!LoggedUsers.Contains(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not logged in.");
                }
                else if(!DbAdmin.Exists(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user to be added.");
                }
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result;
            try
            {
                result = DbCustomer.Add(customer);
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'PostCustomer()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }
            
            if (result)
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest("Customer already exists.");
            }
        }

        [HttpPost]
        [Route("api/users/postDriver")]
        //[Route("postDriver")]
        [ResponseType(typeof(Driver))]
        public IHttpActionResult PostDriver([FromUri]string senderID, [FromBody]DriverModel driverModel)
        {

            Driver driver = new Driver(driverModel.Username, driverModel.Password)
            {
                FirstName = driverModel.FirstName,
                LastName = driverModel.LastName,
                Gender = driverModel.Gender,
                JMBG = driverModel.JMBG,
                Phone = driverModel.Phone,
                Email = driverModel.Email,
                DriversLocation = DbLocation.GetSingleEntityByKey(driverModel.DriversLocationID),
                DriversVehicle = DbVehicle.GetSingleEntityByKey(driverModel.DriversVehicleID),
            };
            driverModel.TaxiDrivesIDs.ForEach(td => driver.TaxiDrives.Add(DbTaxiDrive.GetSingleEntityByKey(td)));

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }
            else if (!DbAdmin.Exists(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result;
            try
            {
                result = DbDriver.Add(driver);
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'PostDriver()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }

            if (result)
            {
                return Ok(driver);
            }
            else
            {
                return BadRequest("Driver already exists.");
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("api/users/putNonDriver")]
        //[Route("putNonDriver")]
        [ResponseType(typeof(IUser))]
        // PUT api/drivers/5
        public IHttpActionResult PutNonDriver([FromUri]string senderID, [FromBody]GeneralUserModel user)
        {
            if(!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            bool result = false;

            if (DbAdmin.Exists(user.Username))
            {
                if(!DbAdmin.Exists(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
                }

                try
                {
                    Admin admin = new Admin(user.Username, user.Password)
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        JMBG = user.JMBG,
                        Phone = user.Phone,
                        Email = user.Email,
                    };
                    user.TaxiDrivesIDs.ForEach(td => admin.TaxiDrives.Add(DbTaxiDrive.GetSingleEntityByKey(td)));

                    result = DbAdmin.Modify(admin as Admin);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'PutNonDriver()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if (DbCustomer.Exists(user.Username))
            { 
                if (!DbAdmin.Exists(senderID) || senderID != user.Username)
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user to be modifed.");
                }

                try
                {
                    Customer customer = new Customer(user.Username, user.Password)
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        JMBG = user.JMBG,
                        Phone = user.Phone,
                        Email = user.Email,
                    };
                    user.TaxiDrivesIDs.ForEach(td => customer.TaxiDrives.Add(DbTaxiDrive.GetSingleEntityByKey(td)));

                    result = DbCustomer.Modify(customer as Customer);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'PutNonDriver()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }

            if (result)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/users/putDriver")]
        //[Route("putDriver")]
        [ResponseType(typeof(Driver))]
        // PUT api/drivers/5
        public IHttpActionResult PutDriver([FromUri]string senderID, [FromBody]DriverModel driverModel)
        {
            
            bool result = false;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            if (DbDriver.Exists(driverModel.Username))
            {
                if (!DbAdmin.Exists(senderID) || senderID != driverModel.Username)
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor a user to be modified.");
                }


                try
                {
                    Driver driver = new Driver(driverModel.Username, driverModel.Password)
                    {
                        FirstName = driverModel.FirstName,
                        LastName = driverModel.LastName,
                        Gender = driverModel.Gender,
                        JMBG = driverModel.JMBG,
                        Phone = driverModel.Phone,
                        Email = driverModel.Email,
                        DriversLocation = DbLocation.GetSingleEntityByKey(driverModel.DriversLocationID),
                        DriversVehicle = DbVehicle.GetSingleEntityByKey(driverModel.DriversVehicleID),
                    };
                    driverModel.TaxiDrivesIDs.ForEach(td => driver.TaxiDrives.Add(DbTaxiDrive.GetSingleEntityByKey(td)));

                    result = DbDriver.Modify(driver);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'PutDriver()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }

            if (result)
            {
                return Ok(driverModel);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("api/users/deleteUser")]
        //[Route("deleteUser")]
        [ResponseType(typeof(void))]
        // DELETE api/drivers/5
        public IHttpActionResult DeleteUser([FromUri]string senderID, [FromUri]string userToDelete)
        {
            bool result = false;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }
            else if(!DbAdmin.Exists(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
            }

            try
            {
                if (DbAdmin.Exists(userToDelete))
                {
                    //TODO: sta ako obrise samog sebe logout?
                    result = DbAdmin.Delete(userToDelete);
                }
                else if (DbDriver.Exists(userToDelete))
                {
                    result = DbDriver.Delete(userToDelete);
                }
                else if (DbCustomer.Exists(userToDelete))
                {
                    result = DbCustomer.Delete(userToDelete);
                }
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'DeleteUser()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }

            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
        #endregion
    }
}