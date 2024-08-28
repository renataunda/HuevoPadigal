using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PadigalAPI.Models;

namespace PadigalAPI.Converters
{
    public class PaymentTypeConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is PaymentType paymentType)
            {
                writer.WriteValue(paymentType.ToString());
            }
            else
            {
                throw new JsonSerializationException("Expected PaymentType object value");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            return Enum.TryParse<PaymentType>(value, true, out var paymentType) ? paymentType : throw new JsonSerializationException("Invalid payment type");
        }
    }
}
