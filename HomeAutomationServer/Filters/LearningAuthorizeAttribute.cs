using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Security.Principal;
using System.Text;
using Ninject;
using HomeAutomationServer.Services;

namespace HomeAutomationServer.Filters
{
    public class LearningAuthorizeAttribute : AuthorizationFilterAttribute
    {

        private UserRepository TheRepository = new UserRepository();

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //Case that user is authenticated using forms authentication 
            //so no need to check header for basic authentication.
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var credArray = GetCredentials(authHeader);
                    var userName = credArray[0];
                    var password = credArray[1];

                    if (TheRepository.GetUser(userName) != null &&
                        TheRepository.GetUser(userName)["password"].ToString().Equals(password))
                    {
                        var currentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                        Thread.CurrentPrincipal = currentPrincipal;
                        return;
                    }
                }
            }

            HandleUnauthorizedRequest(actionContext);
        }

        private string[] GetCredentials(System.Net.Http.Headers.AuthenticationHeaderValue authHeader)
        {

            //Base 64 encoded string
            var rawCred = authHeader.Parameter;
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var cred = encoding.GetString(Convert.FromBase64String(rawCred));

            var credArray = cred.Split(':');

            return credArray;
        }

        private void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Headers.Add("WWW-Authenticate",
                                               "Basic Scheme='eLearning' location='http://localhost:8323/account/login'");

        }
    }
}