using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Security.Claims;

namespace APIKeycloak_Example.API.Controllers
{
    public class ExampleController : ApiController
    {
        [HttpGet]
        [Route("api/example/method1")]
        public HttpResponseMessage method1()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "method1 in api2");
        }

        [HttpGet]
        [Route("api/example/method2")]
        [Authorize(Roles = "role1")]
        public HttpResponseMessage method2()
        {
            var userPrinciple = (System.Security.Claims.ClaimsIdentity)User.Identity;
            return Request.CreateResponse(HttpStatusCode.OK, 
                "method2 in api2, custom1: " + (userPrinciple.HasClaim(c => c.Type == "custom1") ? userPrinciple.Claims.Single(c => c.Type == "custom1").Value : "NULL") +
                ", custom2: " + (userPrinciple.HasClaim(c => c.Type == "custom2") ? userPrinciple.Claims.Single(c => c.Type == "custom2").Value : "NULL"));
        }

        [HttpGet]
        [Route("api/example/method3")]
        [Authorize(Roles = "role1")]
        [Authorize(Roles = "role2")]
        public HttpResponseMessage getSomethingForMultiRoles()
        {
            var userPrinciple = (System.Security.Claims.ClaimsIdentity)User.Identity;
            return Request.CreateResponse(HttpStatusCode.OK,
                "method3 in api2, custom1: " + (userPrinciple.HasClaim(c => c.Type == "custom1") ? userPrinciple.Claims.Single(c => c.Type == "custom1").Value : "NULL") +
                ", custom2: " + (userPrinciple.HasClaim(c => c.Type == "custom2") ? userPrinciple.Claims.Single(c => c.Type == "custom2").Value : "NULL"));
        }
    }
}