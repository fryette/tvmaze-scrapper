using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TvMazeScrapper.Infrastructure.Interfaces.App;

namespace TvMazeScrapper.Services.App
{
    public class NewtonJsonConverter : IJsonConverter
    {
        public T Deserialize<T>(string jsonString, string DateTimeFormat = null)
        {
            if (!string.IsNullOrEmpty(DateTimeFormat))
            {
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = DateTimeFormat };
                return JsonConvert.DeserializeObject<T>(jsonString, dateTimeConverter);
            }

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}
