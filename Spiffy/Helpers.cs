using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spiffy
{
    internal static class Helpers
    {
        internal static async ValueTask<(HttpStatusCode, IPAddress?)> GetIPAddressAsync(Uri uri, Format format)
        {
            (HttpStatusCode status, string? data) = await GetApiResponseAsync(uri).ConfigureAwait(false);

            if (status != HttpStatusCode.OK)
            {
                return (status, null);
            }

            if (format == Format.Text)
            {
                return (status, IPAddress.Parse(data));
            }

            JsonResponse response = JsonSerializer.Deserialize<JsonResponse>(data);

            return (status, response.Value);
        }

        internal static async ValueTask<(HttpStatusCode, string?)> GetApiResponseAsync(Uri uri)
        {
            HttpStatusCode status = HttpStatusCode.Unused;
            string? data = string.Empty;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.UserAgent.ParseAdd(Constants.UserAgent);

            HttpResponseMessage? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    response.EnsureSuccessStatusCode();

                    data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
            catch (HttpRequestException)
            {
                data = null;
            }
            finally
            {
                request.Dispose();

                if (response is not null)
                {
                    status = response.StatusCode;
                    response.Dispose();
                }
                
            }

            return (status, data);
        }       
    }
}