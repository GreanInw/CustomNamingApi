using POC_NamingApi.Helpers;
using System.Text.Json;

namespace POC_NamingApi.NamingPolicy
{
    public sealed class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseJsonNamingPolicy Instance => new();

        public override string ConvertName(string name) => SnakeCaseNamingHelper.ChangeTo(name);
    }
}