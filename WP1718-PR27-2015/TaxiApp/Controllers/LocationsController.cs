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
    //[RoutePrefix("api/locations")]
    public class LocationsController : ApiController
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
        [Route("api/locations/get")]
        //[Route("get")]
        [ResponseType(typeof(IEnumerable<Location>))]
        public IHttpActionResult GetLocations()
        {
            return Ok();
            ////TODO: AUTHORIZATION

            //AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            //CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            //DriverDbAccess dbDriver = DriverDbAccess.Instance;
            //List<IUser> result = new List<IUser>();

            //try
            //{
            //    (dbAdmin.GetAll()).ToList().ForEach(a => result.Add(a));
            //    (dbCustomer.GetAll()).ToList().ForEach(c => result.Add(c));
            //    (dbDriver.GetAll()).ToList().ForEach(d => result.Add(d));
            //}
            //catch (Exception e)
            //{
            //    Trace.Write($"Error on 'GetUsers()'. Error message: {e.Message}");
            //    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //    return InternalServerError(e);
            //}

            //return Ok(result);
        }

        [HttpGet]
        [Route("api/locations/get")]
        //[Route("get")]
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocation(string id)
        {
            return Ok();
            //AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            //Admin result = null;

            //if (!LoggedUsers.Contains(id))
            //{
            //    return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            //}

            //try
            //{
            //    result = dbAdmin.GetSingleEntityByKey(id);
            //}
            //catch (Exception e)
            //{
            //    Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
            //    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //    return InternalServerError(e);
            //}

            //if (result == null)
            //{
            //    return NotFound();
            //}

            //return Ok(result);
        }
        #endregion

        #region POST
        [HttpPost]
        [Route("api/locations/post")]
        //[Route("post")]
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(string senderID, [FromBody]Location location)
        {
            return Ok();
            ////Customer ne pravi sam svoj nalog
            //if (senderID != customer.Username)
            //{
            //    AdminDbAccess dbAdmin = AdminDbAccess.Instance;

            //    if (!LoggedUsers.Contains(senderID))
            //    {
            //        return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            //    }
            //    else if (!dbAdmin.Exists(senderID))
            //    {
            //        return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor the user to be added.");
            //    }
            //}

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;

            //bool result;
            //try
            //{
            //    result = dbCustomer.Add(customer);
            //}
            //catch (Exception e)
            //{
            //    Trace.Write($"Error on 'PostCustomer()'. Error message: {e.Message}");
            //    Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //    return InternalServerError(e);
            //}

            //if (result)
            //{
            //    return Ok(customer);
            //}
            //else
            //{
            //    return BadRequest("Customer already exists.");
            //}
        }
        #endregion

        #region PUT
        [HttpPut]
        [Route("api/locations/put")]
        //[Route("put")]
        [ResponseType(typeof(Location))]
        // PUT api/drivers/5
        public IHttpActionResult PutLocation(string senderID, [FromBody]Location location)
        {
            return Ok();
            //AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            //DriverDbAccess dbDriver = DriverDbAccess.Instance;
            //bool result = false;

            //if (!LoggedUsers.Contains(senderID))
            //{
            //    return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            //}

            //if (dbDriver.Exists(driver.Username))
            //{
            //    if (!dbAdmin.Exists(senderID) || senderID != driver.Username)
            //    {
            //        return Content(HttpStatusCode.Unauthorized, "Not a dispatcher nor a user to be modified.");
            //    }

            //    try
            //    {
            //        result = dbDriver.Modify(driver);
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
            //        Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //        return InternalServerError(e);
            //    }
            //}

            //if (result)
            //{
            //    return Ok(driver);
            //}
            //else
            //{
            //    return NotFound();
            //}
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("api/locations/delete")]
        //[Route("delete")]
        [ResponseType(typeof(void))]
        // DELETE api/drivers/5
        public IHttpActionResult DeleteLocatio(string senderID, [FromBody]string locationToDelete)
        {
            return Ok();
            //AdminDbAccess dbAdmin = AdminDbAccess.Instance;
            //DriverDbAccess dbDriver = DriverDbAccess.Instance;
            //CustomerDbAccess dbCustomer = CustomerDbAccess.Instance;
            //bool result = false;

            //if (!LoggedUsers.Contains(senderID))
            //{
            //    return Content(HttpStatusCode.Unauthorized, "Not logged in.");
            //}
            //else if (!dbAdmin.Exists(senderID))
            //{
            //    return Content(HttpStatusCode.Unauthorized, "Not a dispatcher.");
            //}

            //if (dbAdmin.Exists(userToDelete))
            //{
            //    try
            //    {
            //        //TODO: sta ako obrise samog sebe logout?
            //        result = dbAdmin.Delete(userToDelete);
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
            //        Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //        return InternalServerError(e);
            //    }
            //}
            //else if (dbDriver.Exists(userToDelete))
            //{
            //    try
            //    {
            //        result = dbDriver.Delete(userToDelete);
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
            //        Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //        return InternalServerError(e);
            //    }
            //}
            //else if (dbCustomer.Exists(userToDelete))
            //{
            //    try
            //    {
            //        result = dbCustomer.Delete(userToDelete);
            //    }
            //    catch (Exception e)
            //    {
            //        Trace.Write($"Error on 'GetUser()'. Error message: {e.Message}");
            //        Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            //        return InternalServerError(e);
            //    }
            //}

            //if (result)
            //{
            //    return Ok();
            //}
            //else
            //{
            //    return NotFound();
            //}
        }
        #endregion
    }
}