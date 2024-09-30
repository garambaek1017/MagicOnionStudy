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

        public static string ToLogString<T>(this T obj)
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
        
        public static T ToObject<T>(this string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(jsonString, JsonOptions);
            }
            catch (Exception)
            {
                return default(T); // 역직렬화 실패 시 기본값 반환 (참조형은 null, 값형은 기본값)
            }
        }
        
        public static string ToJson<T>(this T obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, JsonOptions);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
