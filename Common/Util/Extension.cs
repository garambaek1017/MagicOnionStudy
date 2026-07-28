using System;
using Newtonsoft.Json;

namespace Shared.Util
{
    public static class Extension
    {
        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static string ToLogString<T>(this T obj)
        {
            try
            {
                return $"[{obj.GetType().Name}]:{JsonConvert.SerializeObject(obj, JsonSettings)}";
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
                return JsonConvert.DeserializeObject<T>(jsonString, JsonSettings);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string ToJson<T>(this T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, JsonSettings);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
