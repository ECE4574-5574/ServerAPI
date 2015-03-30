using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeAutomationServer.Controllers
{
    [RoutePrefix("api/sim")]
    public class SimController : ApiController
    {

        [Route("configuration/{id}")]
        public object GetConfiguration(int id)
        {
            JObject obj = new JObject();
            obj.Add("Id", id);
            return obj;
        }

    }
}
