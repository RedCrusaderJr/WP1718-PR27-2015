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

        #region GET
        [HttpGet]
        [Route("api/users/get")]
        //[Route("get")]
        [ResponseType(typeof(IEnumerable<IUser>))]
        public IHttpActionResult GetUsers(string senderID)
        {
            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            DriverDbAccess dbDriver = DriverDbAccess.Instance;
            List<IUser> result = new List<IUser>();

            if(!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            //other rights?

            try
            {
                (dbAdmin.GetAll()).ToList().ForEach(a => result.Add(a));
                (dbCustomer.GetAll()).ToList().ForEach(c => result.Add(c));
                (dbDriver.GetAll()).ToList().ForEach(d => result.Add(d));
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
        public IHttpActionResult GetNonDriver(string senderID, [FromBody]string userIdToGet)
        {
            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            IUser result = null;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            if(dbAdmin.Exists(userIdToGet))
            {
                if (!dbAdmin.Exists(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
                }

                try
                {
                    result = dbAdmin.GetSingleEntityByKey(userIdToGet);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if(dbCustomer.Exists(userIdToGet))
            {
                if (!dbAdmin.Exists(senderID) || senderID != userIdToGet)
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user whose information are requested.");
                }

                try
                {
                    result = dbAdmin.GetSingleEntityByKey(userIdToGet);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
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
        public IHttpActionResult GetDriver(string senderID, [FromBody]string userIdToGet)
        {
            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            DriverDbAccess dbDriver = DriverDbAccess.Instance;
            Driver result = null;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            if (dbDriver.Exists(userIdToGet))
            {
                if (!dbAdmin.Exists(senderID) || senderID != userIdToGet)
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user whose information are requested.");
                }

                try
                {
                    result = dbDriver.GetSingleEntityByKey(userIdToGet);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
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
        public IHttpActionResult Login(LoginModel loginModel)
        {
            if (LoggedUsers.Contains(loginModel.Username))
            {
                return Content(HttpStatusCode.Conflict, $"User '{loginModel.Username}' already logged in.");
            }

            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            DriverDbAccess dbDriver = DriverDbAccess.Instance;
            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            IUser result = null;

            if (dbAdmin.Exists(loginModel.Username))
            {
                try
                {
                    result = dbAdmin.GetSingleEntityByKey(loginModel.Username);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if (dbDriver.Exists(loginModel.Username))
            {
                try
                {
                    result = dbDriver.GetSingleEntityByKey(loginModel.Username);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if (dbCustomer.Exists(loginModel.Username))
            {
                try
                {
                    result = dbCustomer.GetSingleEntityByKey(loginModel.Username);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
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
        public IHttpActionResult Logout([FromBody]string senderID)
        {
            if (!LoggedUsers.Contains(senderID))
            {
                return BadRequest($"User '{senderID}' was not logged in.");
            }
            else
            {
                LoggedUsers.Remove(senderID);
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/users/postCustomer")]
        //[Route("postCustomer")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(string senderID, [FromBody]Customer customer)
        { 
            //Customer ne pravi sam svoj nalog
            if (senderID != customer.Username)
            {
                AdminDbAccess dbAdmin = AdminDbAccess.Instance;

                if (!LoggedUsers.Contains(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not logged in.");
                }
                else if(!dbAdmin.Exists(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user to be added.");
                }
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;

            bool result;
            try
            {
                result = dbCustomer.Add(customer);
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
        public IHttpActionResult PostDriver(string senderID, [FromBody]Driver driver)
        {
            AdminDbAccess dbAdmin = AdminDbAccess.Instance;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }
            else if (!dbAdmin.Exists(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DriverDbAccess dbDriver = DriverDbAccess.Instance;

            bool result;
            try
            {
                result = dbDriver.Add(driver);
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'PostCustomer()'. Error message: {e.Message}");
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
        public IHttpActionResult PutNonDriver(string senderID, [FromBody]IUser user)
        {
            if(!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            bool result = false;

            if (dbAdmin.Exists(user.Username))
            {
                if(!dbAdmin.Exists(senderID))
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
                }

                try
                {
                    result = dbAdmin.Modify(user as Admin);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if (dbCustomer.Exists(user.Username))
            { 
                if (!dbAdmin.Exists(senderID) || senderID != user.Username)
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user to be modifed.");
                }

                try
                {
                    result = dbCustomer.Modify(user as Customer);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
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
        public IHttpActionResult PutDriver(string senderID, [FromBody]Driver driver)
        {
            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            DriverDbAccess dbDriver = DriverDbAccess.Instance;
            bool result = false;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            if (dbDriver.Exists(driver.Username))
            {
                if (!dbAdmin.Exists(senderID) || senderID != driver.Username)
                {
                    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor a user to be modified.");
                }

                try
                {
                    result = dbDriver.Modify(driver);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }

            if (result)
            {
                return Ok(driver);
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
        public IHttpActionResult DeleteUser(string senderID, [FromBody]string userToDelete)
        {
            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            DriverDbAccess dbDriver = DriverDbAccess.Instance;
            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            bool result = false;

            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }
            else if(!dbAdmin.Exists(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
            }

            if (dbAdmin.Exists(userToDelete))
            {
                try
                {
                    //TODO: sta ako obrise samog sebe logout?
                    result = dbAdmin.Delete(userToDelete);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if (dbDriver.Exists(userToDelete))
            {
                try
                {
                    result = dbDriver.Delete(userToDelete);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
            }
            else if (dbCustomer.Exists(userToDelete))
            {
                try
                {
                    result = dbCustomer.Delete(userToDelete);
                }
                catch (Exception e)
                {
                    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                    return InternalServerError(e);
                }
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