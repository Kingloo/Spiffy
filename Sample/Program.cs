using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Spiffy;

namespace Sample
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            bool performIPv4Lookup = true;
            bool performIPv6Lookup = true;
            bool prettyPrint = args.Length == 0 || args.Contains("pretty");

            if (args.Length >= 1)
            {
                performIPv4Lookup = args[0] == "ipv4";
                performIPv6Lookup = args[0] == "ipv6";
            }

            if (performIPv4Lookup)
            {
                SpiffyResponse ipv4Response = await Spiffy.IPv4.GetAddressAsync(Format.Text).ConfigureAwait(false);

                await WriteOutResponseAsync(ipv4Response, prettyPrint).ConfigureAwait(false);
            }

            if (performIPv6Lookup)
            {
                SpiffyResponse ipv6Response = await Spiffy.IPv6.GetAddressAsync(Format.Text).ConfigureAwait(false);

                await WriteOutResponseAsync(ipv6Response, prettyPrint).ConfigureAwait(false);
            }

            return 0;
        }

        private static async ValueTask WriteOutResponseAsync(SpiffyResponse response, bool prettyPrint)
        {
            string message = response.Status switch
            {
                HttpStatusCode.OK => prettyPrint switch
                {
                    true => $"your public {GetAddressFamilyNicerName(response.Address?.AddressFamily)} address is {(response.Address?.ToString() ?? "unknown")}",
                    false => response.Address?.ToString() ?? "unknown"
                },
                _ => $"failed ({response.Status})"
            };

            await Console.Out.WriteLineAsync(message).ConfigureAwait(false);
        }

        private static string GetAddressFamilyNicerName(AddressFamily? family)
        {
            return family switch
            {
                AddressFamily.InterNetwork => "IPv4",
                AddressFamily.InterNetworkV6 => "IPv6",
                _ => "other"
            };
        }
    }
}
