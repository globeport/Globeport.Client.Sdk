using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Globeport.Shared.Library.Components
{
    public class DataObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DataObject) || objectType == typeof(object);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    {
                        var result = new DataObject();

                        reader.Read();

                        while (reader.TokenType != JsonToken.EndObject)
                        {
                            var propertyName = (string)reader.Value;
                            reader.Read();
                            result[propertyName] = serializer.Deserialize(reader, objectType);
                            reader.Read();
                        }

                        return result;
                    }
                case JsonToken.StartArray:
                    {
                        var result = new List<object>();

                        reader.Read();

                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            result.Add(serializer.Deserialize(reader, objectType));
                            reader.Read();
                        }

                        return result.ToArray();
                    }
                default:
                    return serializer.Deserialize(reader);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jintObject = (DataObject)value;

            serializer.Serialize(writer, jintObject.Data.ToDictionary(i => i.Key, i => i.Value));
        }
    }
}
