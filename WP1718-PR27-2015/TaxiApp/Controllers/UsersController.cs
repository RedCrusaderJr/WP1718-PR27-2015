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

namespace TaxiApp.Controllers
{
    public class UsersController : ApiController
    {
        private List<string> LoggedUsers
        {
            get
            {
                return HttpContext.Current.Application["Logged"] as List<string>;
            }
        }

        [HttpGet]
        [Route("api/users/get")]
        [ResponseType(typeof(IEnumerable<IUser>))]
        public IHttpActionResult GetUsers()
        {
            //TODO: AUTHORIZATION

            AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            DriverDbAccess dbDriver = DriverDbAccess.Instance;
            List<IUser> result = new List<IUser>();

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
        [Route("api/users/get")]
        [ResponseType(typeof(Admin))]
        public IHttpActionResult GetUser([FromUri]string id)
        {
            AdminDbAccess db = AdminDbAccess.Instance;
            Admin result = null;

            if(!LoggedUsers.Contains(id))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            try
            {
                result = db.GetSingleEntityByKey(id);
            }
            catch (Exception e)
            {
                Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
                return InternalServerError(e);
            }

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/users/login")]
        public IHttpActionResult Login(/*LoginModel*/)
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/users/logout")]
        public IHttpActionResult Logout([FromBody]string senderID)
        {
            return Ok();
        }

        [HttpPost]
        [Route("api/users/postCustomer")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(string senderID, [FromBody]Customer customer)
        {
            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
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
        [ResponseType(typeof(Driver))]
        public IHttpActionResult PostDriver(string senderID, [FromBody]Driver driver)
        {
            if (!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
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

        [HttpPut]
        [Route("api/users/putUser")]
        [ResponseType(typeof(void))]
        // PUT api/drivers/5
        public IHttpActionResult PutUser(string senderID, [FromBody]IUser user)
        {
            //TODO razdvojiti driver/customer/admin ?
            if(!LoggedUsers.Contains(senderID))
            {
                return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            }

            //TODO modify
            return Ok();
        }

        [HttpDelete]
        [Route("api/users/deleteUser")]
        [ResponseType(typeof(void))]
        // DELETE api/drivers/5
        public IHttpActionResult DeleteUser(int id)
        {
            //TODO
            return Ok();
        }
    }
}