using NamingApi.SnakeCase.Attributes;

namespace POC_NamingApi.DTOs
{
    [SnakeCaseObject]
    public class ExamplePostRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }

        public IFormFile ProfileImage { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public IFormFileCollection FileCollection { get; set; }

        public RoleRequest RoleInfo { get; set; } = new RoleRequest();
        public IEnumerable<RoleRequest> Roles { get; set; } = new List<RoleRequest>();


        public class RoleRequest
        {
            public string RoleName { get; set; }
            public string RoleNameDescription { get; set; }
        }
    }
}
