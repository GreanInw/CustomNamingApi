using Microsoft.AspNetCore.Mvc.ModelBinding;
using NamingApi.SnakeCase.Attributes;

namespace POC_NamingApi.DTOs
{
    public class ExamplePostPerPropertyRequest
    {
        [BindNever]
        [SnakeCaseName("first_name")]
        public string FirstName { get; set; }

        [SnakeCaseName("last_name")]
        public string LastName { get; set; }
        [SnakeCaseName("birthday")]
        public DateTime? Birthday { get; set; }

        [SnakeCaseName("profile_image")]
        public IFormFile ProfileImage { get; set; }
        [SnakeCaseName("files")]
        public IEnumerable<IFormFile> Files { get; set; }
        [SnakeCaseName("file_collection")]
        public IFormFileCollection FileCollection { get; set; }

        [SnakeCaseName("role_info")]
        public RolePerPropertyRequest RoleInfo { get; set; } = new RolePerPropertyRequest();
        [SnakeCaseName("roles")]
        public IEnumerable<RolePerPropertyRequest> Roles { get; set; } = new List<RolePerPropertyRequest>();


        public class RolePerPropertyRequest
        {
            [SnakeCaseName("role_name")]
            public string RoleName { get; set; }
            [SnakeCaseName("role_name_description")]
            public string RoleNameDescription { get; set; }
        }
    }
}
