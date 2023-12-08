using System.Text.Json.Serialization;

namespace NamingApi.SnakeCase.Json
{
    public sealed class SnakeCaseJsonOptions
    {
        /// <summary>
        /// Get default <see cref="System.Text.Json.JsonSerializerOptions"/> for snake case and property enum.
        /// </summary>
        /// <returns></returns>
        public static System.Text.Json.JsonSerializerOptions DefaultJsonSerializerOptions
        {
            get
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = SnakeCaseJsonNamingPolicy.Instance,
                    DictionaryKeyPolicy = SnakeCaseJsonNamingPolicy.Instance
                };
                options.Converters.Add(new JsonStringEnumConverter());
                return options;
            }
        }

        /// <summary>
        /// Get default <see cref="Newtonsoft.Json.JsonSerializerSettings"/> for snake case and property enum.
        /// </summary>
        /// <returns></returns>
        public static Newtonsoft.Json.JsonSerializerSettings DefaultJsonSerializerSettings
        {
            get
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    {
                        NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy
                        {
                            OverrideSpecifiedNames = true,
                            ProcessDictionaryKeys = true
                        }
                    }
                };
                settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                return settings;
            }
        }
    }
}