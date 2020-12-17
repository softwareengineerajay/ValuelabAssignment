using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ValueLabtest
{
    public interface IHttpService
    {
        Task<IHttpResponseMessage> GetAsync(string url);
        Task<System.IO.Stream> GetStreamAsync(string url);

    }
    public interface IHttpResponseMessage
    {
        HttpStatusCode StatusCode { get; }
        void EnsureSuccessStatusCode();
        Task<string> ReadAsStringAsync();
    }
}
