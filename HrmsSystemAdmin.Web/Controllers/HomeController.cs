using HrmsSystemAdmin.Web.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HrmsSystemAdmin.Web.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(bool ajaxcall = false)
        {
            ViewBag.AjaxCall = ajaxcall;
            var ismerchant = User.Claims.Where(x => x.Type == "IsMerchant").First().Value;
            if (ismerchant == "True")
                return View("_MerchantDashBoard");

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
