using Newtonsoft.Json;

namespace Cast.Infrastructure
{
    public sealed class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            DateFormatString = Defines.DATE_FORMAT;
            NullValueHandling = NullValueHandling.Ignore;
        }
    }
}