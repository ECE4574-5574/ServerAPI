using HomeAutomationServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeAutomationServerAPI.Controllers
{
    public class HouseController : ApiController
    {
        // GET api/house
        public IEnumerable<HouseModel> Get()
        {
            return null;
        }

        // GET api/house/5
        public HouseModel Get(int id)
        {
            return null;
        }

        // GET api/house/name
        public HouseModel Get(string name)
        {
            return null;
        }

        // POST api/house
        public void Post([FromBody]string value)
        {
        }

        // PUT api/house/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/house/5
        public void Delete(int id)
        {
        }
    }
}
