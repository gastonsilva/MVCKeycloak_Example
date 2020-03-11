using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace MVCKeycloak_Example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var userPrinciple = User as ClaimsPrincipal;

            return View(userPrinciple);
        }

        [Authorize]
        public ActionResult Contact()
        {
            var userPrinciple = User as ClaimsPrincipal;
            if (userPrinciple.HasClaim(c => c.Type == "MyCustomClaim"))
                ViewBag.Message = "My Custom Claim is '" + userPrinciple.Claims.Single(c => c.Type == "MyCustomClaim").Value + "'";
            else
                ViewBag.Message = "My Custom Claim is missing D:";
            return View();
        }
    }
}