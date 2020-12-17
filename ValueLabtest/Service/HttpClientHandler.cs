using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ValueLabtest.Service
{
    public class HttpClientHandler : IHttpService
    {
        private readonly HttpClient _client;

        public HttpClientHandler(HttpClient httpClient)
        {
            this._client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IHttpResponseMessage> GetAsync(string url)
        {
            return new HttpResponseMessageFacade(await _client.GetAsync(url));
        }
        public Task<System.IO.Stream> GetStreamAsync(string url)
        {
            return _client.GetStreamAsync(url);
        }

       

    }
}
