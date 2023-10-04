using Microsoft.AspNetCore.Mvc;
using POC_NamingApi.Attributes;

namespace POC_NamingApi.Controllers
{
    [ApiController, Route("sample")]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] SampleRequest request) => Ok(request);

        [HttpPost]
        public IActionResult Post([FromForm] SamplePostRequest request) => Ok(request);
    }

    public class SampleRequest
    {
        public int Id { get; set; }

        [FromQuery(Name = "first_name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [SnakeCaseObject]
    public class SamplePostRequest
    {
        [FromForm(Name = "first_name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}