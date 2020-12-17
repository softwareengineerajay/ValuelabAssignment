using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ValueLabtest.Model;

namespace ValueLabtest.Service
{
    public class StockServices : IStockServices
    {
        private readonly IHttpClientFactory _clientFactory;

        public StockServices(IHttpClientFactory clientFactory)
        {
           
            _clientFactory= clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

        }
        public async Task<List<StockModel>> GetQuoteDetailAsync(string apiurl)
        {

            if (string.IsNullOrEmpty(apiurl))
                throw new ArgumentNullException(nameof(apiurl));
            List<StockModel> stockModels = null;
            string messageResut = string.Empty;
            try
            {
                var client = _clientFactory.CreateClient();
                var responseMessage = await client.GetAsync(apiurl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    messageResut = responseMessage.Content.ReadAsStringAsync().Result;
                    stockModels= JsonConvert.DeserializeObject<List<StockModel>>(messageResut);

                } 
            }
            catch(Exception exe)
            {
                throw exe;
            }

            return stockModels;
    }

       



    }
}
