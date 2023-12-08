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

        [HttpPost("by_json")]
        public IActionResult PostJson([FromBody] ExampleJsonRequest request) => Ok(request);

    }
}