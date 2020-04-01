using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http;
using System.Security.Claims;
using System.Configuration;
using System.Net.Http.Headers;

namespace APIKeycloak_Example.API.Controllers
{
    public class ExampleController : ApiController
    {
        [HttpGet]
        [Route("api/example/mehod1")]
        public HttpResponseMessage method1()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "mehod1 in api1");
        }

        [HttpGet]
        [Route("api/example/method2")]
        [Authorize(Roles = "role1")]
        public HttpResponseMessage method2()
        {
            var userPrinciple = (System.Security.Claims.ClaimsIdentity)User.Identity;

            var token = userPrinciple.Claims.Single(c => c.Type == "access_token").Value;
            var custom1 = userPrinciple.HasClaim(c => c.Type == "custom1") ? userPrinciple.Claims.Single(c => c.Type == "custom1").Value : "NULL";
            var custom2 = userPrinciple.HasClaim(c => c.Type == "custom2") ? userPrinciple.Claims.Single(c => c.Type == "custom2").Value : "NULL";
            var response2 = "";

            var client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api2-url"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = client.GetAsync("/api/example/method2").Result;
            if (response.IsSuccessStatusCode)
                response2 = response.Content.ReadAsStringAsync().Result;
            else
                response2 = string.Format("Statuscode: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            client.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, 
                $"method2 in api1, token: {token}, custom1: {custom1}, custom2: {custom2}, response from api2: {response2}");
        }
    }
}