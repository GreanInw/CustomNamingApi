using System.Globalization;

namespace POC_NamingApi.Extensions
{
    public static class InternalExtensions
    {
        public static string SnakeCaseToPascalCase(this string value)
            => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.Replace("_", " ")).Replace(" ", "");
    }
}
