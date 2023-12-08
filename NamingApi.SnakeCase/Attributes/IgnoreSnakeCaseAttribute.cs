namespace NamingApi.SnakeCase.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IgnoreSnakeCaseAttribute : Attribute
    { }
}
