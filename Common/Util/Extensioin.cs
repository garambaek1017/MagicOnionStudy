using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Util
{
    public static class Extension
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        public static string ToString<T>(this T obj)
        {
            try
            {
                return $"[{obj.GetType().Name}]:{JsonSerializer.Serialize(obj, JsonOptions)}";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        
    }
}
