using System.Globalization;

namespace NamingApi.SnakeCase.Extensions
{
    public static class InternalExtensions
    {
        public static string SnakeCaseToPascalCase(this string value)
            => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Replace("_", " ")).Replace(" ", "");
    }
}
