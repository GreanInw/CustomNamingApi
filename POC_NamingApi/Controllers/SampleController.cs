using Microsoft.AspNetCore.Mvc;
using POC_NamingApi.DTOs;

namespace POC_NamingApi.Controllers
{
    [ApiController, Route("sample")]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] ExampleGetRequest request) => Ok(request);

        [HttpPost]
        public IActionResult Post([FromForm] ExamplePostRequest request) => Ok(request);

        [HttpPost("per_property")]
        public IActionResult PerProperty([FromForm] ExamplePostPerPropertyRequest request) => Ok(request);

        //[HttpPost("original")]
        //public IActionResult Post([FromForm] OriginalRequest request) => Ok(request);
    }

    //public class OriginalRequest
    //{
    //    public string FirstName { get; set; }
    //    public IEnumerable<IFormFile> Files { get; set; }
    //    public IFormFileCollection FileCollection { get; set; }
    //    public IEnumerable<RoleRequest> Roles { get; set; } = new List<RoleRequest>();
    //}
}