using HomeAutomationServer.Services;
using HomeAutomationServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using HomeAutomationServer.Filters;

namespace HomeAutomationServer.Controllers
{
    public class LogfileController : ApiController
    {
        // GET api/logfile
        /// <summary>
        /// Gets the logfile as a string
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/logfile/getlog")]
        public string Get()
        {
            return LogFile.GetLog();
        }

        // Get api/logfile/count
        /// <summary>
        /// Gets the logfile count
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("Count"), Route("api/logfile/count")]
        public int GetCount()
        {
            return LogFile.GetCount();
        }

        // Delete api/logfile
        /// <summary>
        /// Deletes the logile
        /// </summary>
        [HttpDelete, Route("api/logfile/delete")]
        public void Delete()
        {
            LogFile.DeleteLog();
        }
    }
}
