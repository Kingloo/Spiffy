using System.Net;

namespace Spiffy
{
    public class SpiffyResponse
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.Unused;
        public IPAddress? Address { get; set; } = IPAddress.None;

        public SpiffyResponse() { }
        
        public SpiffyResponse(HttpStatusCode status, IPAddress? address)
        {
            Status = status;
            Address = address;
        }
    }   
}