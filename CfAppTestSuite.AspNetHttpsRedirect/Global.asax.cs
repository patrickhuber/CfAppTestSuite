using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CfAppTestSuite.AspNetHttpsRedirect
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnforceHttps();

            var response = HttpContext.Current.Response;
            response.Write("Hello World!");
            response.StatusCode = (int)HttpStatusCode.OK;
            response.End();
        }

        private static void EnforceHttps()
        {
            var context = HttpContext.Current;
            var request = context.Request;
            if (request.IsSecureConnection)
                return;

            if (string.Equals(
                request.Headers["X-Forwarded-Proto"],
                Uri.UriSchemeHttps,
                StringComparison.OrdinalIgnoreCase))
                return;

            // uncomment for local dev
            //if (request.IsLocal)
            //    return;

            var response = HttpContext.Current.Response;
            response.RedirectPermanent($"{Uri.UriSchemeHttps}{Uri.SchemeDelimiter}//{request.Url.Authority}{request.Url.PathAndQuery}");
            context.ApplicationInstance.CompleteRequest();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}