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

    [SnakeCaseObject]
    public class SampleRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [SnakeCaseObject]
    public class SamplePostRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public IFormFile ProfileImage { get; set; }
        public RoleRequest RoleInfo { get; set; } = new RoleRequest();
        public IEnumerable<RoleRequest> Roles { get; set; } = new List<RoleRequest>();

        public class RoleRequest
        {
            public string RoleName { get; set; }
            public string RoleNameDescription { get; set; }
        }
    }
}