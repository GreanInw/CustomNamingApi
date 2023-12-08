using NamingApi.SnakeCase.Extensions;
using System.Text;

namespace NamingApi.SnakeCase.Builders
{
    internal sealed class SnakeCaseBuilder
    {
        public const char CombineSymbol = '.';

        public static string Build(string source)
            => IsNestedProperties(source) ? BuildNestedProperties(source) : source.ToSnakeCase();

        internal static bool IsNestedProperties(string source) => source.Contains(CombineSymbol);

        internal static string BuildNestedProperties(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (!IsNestedProperties(source))
            {
                return source;
            }

            var newValue = new StringBuilder();
            foreach (var value in source.Split(CombineSymbol))
            {
                newValue.Append($"{(newValue.Length == 0 ? "" : CombineSymbol)}{value.ToSnakeCase()}");
            }

            return newValue.ToString();
        }
    }
}
