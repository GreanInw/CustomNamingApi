namespace NamingApi.SnakeCase.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SnakeCaseNameAttribute : SnakeCaseObjectAttribute
    {
        public SnakeCaseNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}