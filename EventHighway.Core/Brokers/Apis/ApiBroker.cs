// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventHighway.Core.Brokers.Apis
{
    internal class ApiBroker : IApiBroker
    {
        private readonly HttpClient httpClient;

        public ApiBroker() =>
            this.httpClient = new HttpClient();

        public async ValueTask<string> PostAsync(string content, string url, string secret)
        {
            var stringContent =
               new StringContent(
                   content,
                   encoding: Encoding.UTF8,
                   mediaType: "application/json");

            this.httpClient.DefaultRequestHeaders.Add("X-Highway", secret);

            HttpResponseMessage httpResponseMessage =
                await this.httpClient.PostAsync(
                    requestUri: url,
                    content: stringContent);

            return httpResponseMessage.ToString();
        }
    }
}