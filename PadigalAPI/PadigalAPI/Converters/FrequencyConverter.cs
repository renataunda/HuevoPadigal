using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PadigalAPI.Models;

namespace PadigalAPI.Converters
{
    public class FrequencyConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Frequency frequency)
            {
                writer.WriteValue(frequency.ToString());
            }
            else
            {
                throw new JsonSerializationException("Expected Frequency object value");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            return Enum.TryParse<Frequency>(value, true, out var frequency) ? frequency : throw new JsonSerializationException("Invalid frequency");
        }
    }
}
