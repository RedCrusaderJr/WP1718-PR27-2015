using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TaxiApp.Database_Management.Access;
using TaxiApp.Models;

namespace TaxiApp.Controllers
{
    //[RoutePrefix("api/taxiDrives")]
    public class TaxiDrivesController : ApiController
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
        [Route("api/taxiDrives/get")]
        //[Route("get")]
        [ResponseType(typeof(IEnumerable<TaxiDrive>))]
        public IHttpActionResult GetTaxiDrives()
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
        [Route("api/taxiDrives/get")]
        //[Route("get")]
        [ResponseType(typeof(TaxiDrive))]
        public IHttpActionResult GetTaxiDrive(string id)
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
        [Route("api/taxiDrives/post")]
        //[Route("post")]
        [ResponseType(typeof(TaxiDrive))]
        public IHttpActionResult PostTaxiDrive(string senderID, [FromBody]TaxiDrive location)
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
        [Route("api/taxiDrives/put")]
        //[Route("put")]
        [ResponseType(typeof(TaxiDrive))]
        // PUT api/drivers/5
        public IHttpActionResult PutTaxiDrive(string senderID, [FromBody]TaxiDrive location)
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
        [Route("api/taxiDrives/delete")]
        //[Route("delete")]
        [ResponseType(typeof(void))]
        // DELETE api/drivers/5
        public IHttpActionResult DeleteTaxiDrive(string senderID, [FromBody]string locationToDelete)
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