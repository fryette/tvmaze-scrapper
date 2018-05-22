using Cast.Infrastructure;
using Newtonsoft.Json;

namespace Cast
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