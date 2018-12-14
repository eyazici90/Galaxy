using Galaxy.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json;

namespace Galaxy.Utf8Json
{ 
     public class Utf8JsonSerializer : ISerializer
    {
        public string Serialize(object obj, bool camelCase = true, bool indented = false)
        {
            return JsonSerializer.ToJsonString(obj);
        }

        public T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public object Deserialize(string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize<object>(jsonString);
        }

        public object Deserialize(Type type, string jsonString, bool camelCase = true)
        {
            return JsonSerializer.Deserialize<object>(jsonString);
        }
    }
}
