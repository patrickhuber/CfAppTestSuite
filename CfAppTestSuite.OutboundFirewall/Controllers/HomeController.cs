using CfAppTestSuite.OutboundFirewall.Models;
using Microsoft.AspNetCore.Mvc;

namespace CfAppTestSuite.OutboundFirewall.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        public IActionResult Index(PortIpQueryModel[] query)
        {

            return NotFound();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
