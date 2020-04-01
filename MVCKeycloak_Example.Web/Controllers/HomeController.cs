using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.IO;
using System.Text;

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
            ViewBag.Message = "Your token information";

            var userPrinciple = User as ClaimsPrincipal;

            return View(userPrinciple);
        }

        [Authorize]
        public ActionResult CallApi1()
        {
            var userPrinciple = User as ClaimsPrincipal;
            if (userPrinciple.HasClaim(c => c.Type == "access_token"))
            {
                var token = userPrinciple.Claims.Single(c => c.Type == "access_token").Value;
                ViewBag.Token = $"Your access token is: '{token}'";

                var client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api1-url"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync("/api/example/method2").Result;
                if (response.IsSuccessStatusCode)
                    ViewBag.CallApi1 = response.Content.ReadAsStringAsync().Result;
                else
                    ViewBag.CallApi1 = string.Format("Statuscode: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                client.Dispose();
            }
            else
                ViewBag.Message = "Acces token missing";
            return View();
        }
    }
}