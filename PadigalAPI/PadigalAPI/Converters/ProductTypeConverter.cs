using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PadigalAPI.Models;

namespace PadigalAPI.Converters
{
    public class ProductTypeConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is ProductType productType)
            {
                writer.WriteValue(productType.ToString());
            }
            else
            {
                throw new JsonSerializationException("Expected ProductType object value");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();
            return Enum.TryParse<ProductType>(value, true, out var productType) ? productType : throw new JsonSerializationException("Invalid product type");
        }
    }
}
