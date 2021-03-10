using System.Net;
using System.Text.Json.Serialization;
using Spiffy.Json;

namespace Spiffy
{
    public class JsonResponse
    {
        [JsonConverter(typeof(IPAddressJsonConverter))]
        public IPAddress Value { get; set; } = IPAddress.None;
        
        public JsonResponse() { }

        public JsonResponse(IPAddress value)
        {
            Value = value;
        }
    }
}