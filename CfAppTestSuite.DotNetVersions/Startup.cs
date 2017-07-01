using Microsoft.Owin;
using Microsoft.Win32;
using Owin;

[assembly: OwinStartup(typeof(CfAppTestSuite.DotNetVersions.Startup))]

namespace CfAppTestSuite.DotNetVersions
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(async (context) => 
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("\"");
                await context.Response.WriteAsync(
                    Get45PlusFromRegistry());
                await context.Response.WriteAsync("\"");
            });
        }

        private static string Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    return ".NET Framework Version: " + CheckFor45PlusVersion((int)ndpKey.GetValue("Release"));
                }
                else
                {
                    return ".NET Framework Version 4.5 or later is not detected.";
                }
            }
        }

        // Checking the version using >= will enable forward compatibility.
        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 460798)
                return "4.7 or later";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
            {
                return "4.6.1";
            }
            if (releaseKey >= 393295)
            {
                return "4.6";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5";
            }
            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }
    }
}
