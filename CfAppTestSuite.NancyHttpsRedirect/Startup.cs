﻿using CfAppTestSuite.NancyHttpsRedirect;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace CfAppTestSuite.NancyHttpsRedirect
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(options=>options.Bootstrapper = new Bootstrapper());
        }
    }
}