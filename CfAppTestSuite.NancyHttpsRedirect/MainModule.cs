using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CfAppTestSuite.NancyHttpsRedirect
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            this.RequiresHttps(redirect: true);
            Get["/"] = _ => "Hello World";
        }
    }
}