using NamingApi.SnakeCase.Helpers;
using System.Text.Json;

namespace NamingApi.SnakeCase.Json
{
    public sealed class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseJsonNamingPolicy Instance => new();

        public override string ConvertName(string name) => SnakeCaseNamingHelper.ChangeTo(name);
    }
}