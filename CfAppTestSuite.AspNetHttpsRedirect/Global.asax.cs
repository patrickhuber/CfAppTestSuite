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
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            var application = HttpContext.Current.ApplicationInstance;

            if (Match(request, "/health"))
            {
                Health(request, response);
            }
            else
            {
                EnforceHttps(request, application);
                Default(request, response);
            }
        }

        private static void Default(HttpRequest request, HttpResponse response)
        {
            response.Write("Hello World!");
            Ok(response);
        }

        private static void Health(HttpRequest request, HttpResponse response)
        {
            response.Write("Healthy");
            Ok(response);
        }

        private static void Ok(HttpResponse response)
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            response.End();
        }


        private static bool Match(HttpRequest request, string route)
        {
            // split the route into segments 
            var routeSegments = route
                .Split('/')
                .Select(x=>x.ToLower())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            // normalize the request path
            var normalizedSegments = request.Url
                .Segments
                .Select(x => x.Trim('/'))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            if (routeSegments.Length > normalizedSegments.Length)
                return false;

            // just match the route to the request path, ignore any additional path information
            // in the request uri after the route pattern is done matching
            for (var i = 0; i < routeSegments.Length; i++)
            {
                if (!routeSegments[i].Equals(normalizedSegments[i]))
                    return false;
            }
            return true;
        }


        private static void EnforceHttps(HttpRequest request, HttpApplication application)
        {
            if (request.IsSecureConnection)
                return;

            if (string.Equals(
                request.Headers["X-Forwarded-Proto"],
                Uri.UriSchemeHttps,
                StringComparison.OrdinalIgnoreCase))
                return;
            
            // uncomment for local dev
            if (request.IsLocal)
                return;

            var response = HttpContext.Current.Response;
            response.RedirectPermanent($"{Uri.UriSchemeHttps}{Uri.SchemeDelimiter}//{request.Url.Host}:443{request.Url.PathAndQuery}");
            application.CompleteRequest();
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