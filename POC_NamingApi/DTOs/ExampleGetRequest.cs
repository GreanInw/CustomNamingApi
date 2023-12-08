using Microsoft.AspNetCore.Mvc.ModelBinding;
using NamingApi.SnakeCase.Attributes;

namespace POC_NamingApi.DTOs
{
    //[SnakeCaseObject]
    public class ExampleGetRequest
    {
        [BindNever]
        [SnakeCaseName("id")]
        public int Id { get; set; }
        [SnakeCaseName("first_name")]
        public string FirstName { get; set; }
        [SnakeCaseName("last_name")]
        public string LastName { get; set; }
    }

    //public class OriginalRequest
    //{
    //    public string FirstName { get; set; }
    //    public IEnumerable<IFormFile> Files { get; set; }
    //    public IFormFileCollection FileCollection { get; set; }
    //    public IEnumerable<RoleRequest> Roles { get; set; } = new List<RoleRequest>();
    //}
}