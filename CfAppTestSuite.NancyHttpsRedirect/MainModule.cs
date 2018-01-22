using Nancy;
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
            Get["/"] = _ => "Hello World";
        }
    }
}