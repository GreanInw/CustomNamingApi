namespace POC_NamingApi.Helpers
{
    public sealed class TypeConverterHelper
    {
        public static object ConvertTo(string value, Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            bool isNullable = underlyingType is not null;
            var validateNumberic = new ValidateNumeric(value, isNullable, isNullable ? underlyingType : type);

            if (validateNumberic.IsValid)//Is numeric.
            {
                //If is nullable type and null value return null, otherwise return value.
                return validateNumberic.Value;
            }
            else
            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return DateTime.TryParse(value, out DateTime newValue) ? newValue
                    : (isNullable ? null : DateTime.MinValue);
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return bool.TryParse(value, out bool newValue) ? newValue
                    : (isNullable ? null : false);
            }
            else
            {
                return value;
            }
        }

        internal class ValidateNumeric
        {
            private readonly bool _isNullable;
            private readonly Type _originalType;
            private readonly string _value;

            public static Type[] NumericTypes => new[]
            {
                typeof(int), typeof(long), typeof(short),
                typeof(float), typeof(double), typeof(decimal)
            };

            public ValidateNumeric(string value, bool isNullable, Type originalType)
            {
                _value = value;
                _isNullable = isNullable;
                _originalType = originalType;
                Initialize();
            }

            public bool IsEmpty { get; set; }
            public bool IsValid { get; set; }
            public object Value { get; set; }

            private void Initialize()
            {
                IsEmpty = string.IsNullOrWhiteSpace(_value);
                IsValid = NumericTypes.Any(w => w == _originalType);
                Value = IsValid && !IsEmpty
                    ? Convert.ChangeType(_value, _originalType)
                    : (_isNullable ? null : 0);
            }
        }
    }
}
