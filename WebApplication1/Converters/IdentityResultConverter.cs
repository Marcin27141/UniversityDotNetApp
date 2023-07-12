using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace WebApplication1.Converters
{
    public class IdentityResultConverter : JsonConverter<IdentityResult>
    {
        public override IdentityResult ReadJson(JsonReader reader, Type objectType, IdentityResult existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            // Get the 'succeeded' property
            var succeeded = (bool)jsonObject["succeeded"];

            // Get the 'errors' property
            var errorsArray = (JArray)jsonObject["errors"];
            var errors = new List<IdentityError>();

            if (errorsArray != null)
            {
                foreach (var error in errorsArray)
                {
                    var code = (string)error["code"];
                    var description = (string)error["description"];
                    var identityError = new IdentityError { Code = code, Description = description };
                    errors.Add(identityError);
                }
            }

            // Create the IdentityResult object
            var result = succeeded ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());

            return result;
        }

        public override void WriteJson(JsonWriter writer, IdentityResult value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
