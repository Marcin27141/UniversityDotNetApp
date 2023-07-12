using System.Security.Claims;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace UniversityApi.JsonConverters
{
    public class ClaimConverter : JsonConverter<Claim>
    {
        public override Claim Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
            var type = jsonObject.GetProperty("Type").GetString();
            var value = jsonObject.GetProperty("Value").GetString();
            var valueType = jsonObject.GetProperty("ValueType").GetString();
            var issuer = jsonObject.GetProperty("Issuer").GetString();
            var originalIssuer = jsonObject.GetProperty("OriginalIssuer").GetString();

            return new Claim(type, value, valueType, issuer, originalIssuer);
        }

        public override void Write(Utf8JsonWriter writer, Claim value, JsonSerializerOptions options)
        {
            throw new NotSupportedException("Writing of Claim objects is not supported.");
        }
    }

}
