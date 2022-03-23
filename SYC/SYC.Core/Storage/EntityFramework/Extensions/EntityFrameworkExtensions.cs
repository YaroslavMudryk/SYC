using System.Text.Json;
namespace SYC.Core.Storage.EntityFramework.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static string ToJson(this object date)
        {
            return JsonSerializer.Serialize(date);
        }

        public static T FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}