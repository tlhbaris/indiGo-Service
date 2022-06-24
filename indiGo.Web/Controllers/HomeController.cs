using indiGo.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace indiGo.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            if (!(!HttpContext.User.Identity.IsAuthenticated || HttpContext.User.IsInRole(Roles.Customer) || HttpContext.User.IsInRole(Roles.Passive)))
            {
                if (HttpContext.User.IsInRole(Roles.Admin))
                {
                    return RedirectToAction("Products", "Admin");
                }
                if (HttpContext.User.IsInRole(Roles.Operator))
                {
                    return RedirectToAction("ServiceDemands", "Operator");
                }
                if(HttpContext.User.IsInRole(Roles.ElectricalService) || HttpContext.User.IsInRole(Roles.GasService) || HttpContext.User.IsInRole(Roles.GasService))
                {
                    return RedirectToAction("WaitingDemands", "Service");
                }
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "CUSTOMER,PASSIVE")]
        public IActionResult ServiceDemand()
        {
            return View();
        }
    }
}
