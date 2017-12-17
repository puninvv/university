using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPocket.Serialization
{
    internal class JsonSerializerSettingsSingletone
    {
        public static JsonSerializerSettings Instance { get; } = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            SerializationBinder = SerializationBinder.Instance,
        };
    }

    internal class SerializationBinder : ISerializationBinder
    {
        public static ISerializationBinder Instance { get; } = new SerializationBinder();

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.Assembly.ToString();
            typeName = serializedType.ToString();
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(typeName, true);
        }
    }
}
