using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TvMazeScrapper.Infrastructure.Serializers
{
    public interface IJsonConverter
    {
        T Deserialize<T>(string stringToDeserialize, string dateTimeFormat = null);
        string Serialize(object objectToSerialize);
    }
}
