using Nancy;
using Nancy.Security;

namespace CfAppTestSuite.NancyHttpsRedirect
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            this.RequiresHttps(redirect: true);
            Get["/test"] = _ =>
            {
                return "Test Successful";
            };
        }
    }
}