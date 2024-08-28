using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PadigalAPI.Models;

namespace PadigalAPI.Converters
{
    public class ClientTypeConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is ClientType clientType)
            {
                writer.WriteValue(clientType.ToString());
            }
            else
            {
                throw new JsonSerializationException("Expected ClientType object value");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            return Enum.TryParse<ClientType>(value, true, out var clientType) ? clientType : throw new JsonSerializationException("Invalid client type");
        }
    }


}
