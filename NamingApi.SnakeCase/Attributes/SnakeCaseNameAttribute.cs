namespace NamingApi.SnakeCase.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SnakeCaseNameAttribute : Attribute
    {
        public SnakeCaseNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}