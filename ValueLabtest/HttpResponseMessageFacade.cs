using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ValueLabtest
{
    public class HttpResponseMessageFacade : IHttpResponseMessage
    {
        private readonly HttpResponseMessage response;

        public HttpResponseMessageFacade(HttpResponseMessage response)
        {
            this.response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public HttpStatusCode StatusCode => response.StatusCode;

        HttpStatusCode IHttpResponseMessage.StatusCode => throw new NotImplementedException();

        public void EnsureSuccessStatusCode()
        {
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> ReadAsStringAsync()
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}
