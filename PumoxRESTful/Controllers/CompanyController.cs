using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumoxRESTful.Models;

namespace PumoxRESTful.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CompanyController : ControllerBase
    {
        //[HttpPost("create")]
        //public ActionResult Create(Company company)
        //{

        //}

        public static string Status { get; set; } = "Testowy status";
        [HttpGet()]
        public string Get()
        {
            Console.WriteLine("Request received: GET /HealthStatus");
            return Status.ToString();
        }

        [BasicAuthorize]
        [HttpPost("SetResponse/{status}")]
        public ActionResult SetResponse(string status)
        {
            Console.WriteLine("Request received: POST /HealthStatus");
            Status = status;
            return Ok($"Changed status to {status}");
        }
    }
}
