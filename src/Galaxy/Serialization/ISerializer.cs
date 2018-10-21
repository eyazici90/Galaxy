using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj, bool camelCase = true, bool indented = false);

        T Deserialize<T>(string jsonString, bool camelCase = true);

        object Deserialize(Type type, string jsonString, bool camelCase = true);

        object Deserialize(string jsonString, bool camelCase = true);
    }
}
