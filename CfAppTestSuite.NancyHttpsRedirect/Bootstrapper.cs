using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Security;
using Nancy.TinyIoc;

namespace CfAppTestSuite.NancyHttpsRedirect
{
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            SSLProxy.RewriteSchemeUsingForwardedHeaders(pipelines);
        }    
    }
}