using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CfAppTestSuite.NancyHttpsRedirect
{
    public class HealthModule : NancyModule
    {
        public HealthModule()
        {
            Get["/health"] = _ => "Healthy";
        }
    }
}