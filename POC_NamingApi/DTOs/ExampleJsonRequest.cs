using Newtonsoft.Json;

namespace POC_NamingApi.DTOs
{
    [JsonObject]
    public class ExampleJsonRequest
    {
        public string OrderNo { get; set; }
        public string OrderName { get; set; }
        public string OrderDate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string InternalId { get; set; }
    }
}