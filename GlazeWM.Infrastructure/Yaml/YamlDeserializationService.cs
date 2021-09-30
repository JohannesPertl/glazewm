using System.IO;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace GlazeWM.Infrastructure.Yaml
{
  public class YamlDeserializationService
  {
    IDeserializer _yamlDeserializer = new DeserializerBuilder()
      .WithNamingConvention(PascalCaseNamingConvention.Instance)
      .Build();

    ISerializer _jsonSerializer = new SerializerBuilder()
      .JsonCompatible()
      .Build();

    /// <summary>
    /// The YAML deserializing library doesn't have support for polymorphic objects. Because of
    /// this, the YAML is first converted into JSON and then deserialized via `Newtonsoft.Json`.
    /// </summary>
    public T Deserialize<T>(TextReader input)
    {
      var yamlObject = _yamlDeserializer.Deserialize(input);
      var jsonString = _jsonSerializer.Serialize(yamlObject);

      return JsonConvert.DeserializeObject<T>(jsonString);
    }
  }
}

