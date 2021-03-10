using System.Net;
using System.Threading.Tasks;

namespace Spiffy
{
    public static class IPv4
    {
        public static ValueTask<SpiffyResponse> GetAddressAsync()
            => GetAddressAsync(Format.Text);

        public static async ValueTask<SpiffyResponse> GetAddressAsync(Format format)
        {
            (HttpStatusCode status, IPAddress? ipv4Address) = await Helpers.GetIPAddressAsync(Constants.IPv4Endpoint, format).ConfigureAwait(false);

            return new SpiffyResponse(status, ipv4Address);
        }
    }

    public static class IPv6
    {
        public static ValueTask<SpiffyResponse> GetAddressAsync()
            => GetAddressAsync(Format.Text);

        public static async ValueTask<SpiffyResponse> GetAddressAsync(Format format)
        {
            (HttpStatusCode status, IPAddress? ipv6Address) = await Helpers.GetIPAddressAsync(Constants.IPv6Endpoint, format).ConfigureAwait(false);

            return new SpiffyResponse(status, ipv6Address);
        }
    }
}