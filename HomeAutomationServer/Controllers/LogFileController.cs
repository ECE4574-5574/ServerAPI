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
    public class LogFileController : ApiController
    {
        // GET api/logfile
        /// <summary>
        /// Gets the logfile as a string
        /// </summary>
        /// <returns></returns>
        [Route("api/logfile")]
        public string GetLogFile()
        {
            return LogFile.GetLog();
        }

        // Get api/logfile/count
        /// <summary>
        /// Gets the logfile count
        /// </summary>
        /// <returns></returns>
        [Route("count")]
        public int GetLogCount()
        {
            return LogFile.GetCount();
        }

        // Delete api/logfile
        /// <summary>
        /// Deletes the logile
        /// </summary>
        [Route("api/logfile")]
        public void DeleteLogFile()
        {
            LogFile.DeleteLog();
        }
    }
}
