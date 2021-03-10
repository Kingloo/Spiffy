using System;

namespace Spiffy
{
    public static class Constants
    {
        public const string UserAgent = "Spiffy .NET library (https://github.com/Kingloo/Spiffy)";
        public static readonly Uri IPv4Endpoint = new Uri("https://api.ipify.org");
        public static readonly Uri IPv6Endpoint = new Uri("https://api6.ipify.org");
    }
}