using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TaxiApp.Controllers
{
    public class TaxiDrivesController : ApiController
    {
        // GET api/drivers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/drivers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/drivers
        public void Post([FromBody]string value)
        {
        }

        // PUT api/drivers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/drivers/5
        public void Delete(int id)
        {
        }
    }
}