using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ValueLabtest.Service;

namespace ValueLabtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockServices _stockServices;

        public StockController(IStockServices stockServices)
        {
            _stockServices = stockServices ?? throw new ArgumentNullException(nameof(stockServices)); ;

        }
        public async Task<IActionResult> GetStockItemsAsync()
        {
            var url = "https://financialmodelingprep.com/api/v3/quote/AAWW, AAL, CPAAW,PRAA, PAAS, RYAAY ?apikey=b351eb4b7226aefb40eb0da9df7cc616";
            var quoteResult = await _stockServices.GetQuoteDetailAsync(url);
            if (quoteResult == null ||quoteResult.Count==0)
                return NotFound();
            var maxQuotePrice = quoteResult.Max(x => x.Price);

            var avgChangesPercentage = quoteResult.Average(x => x.ChangesPercentage);
            return Ok(new { maxQuotePrice, avgChangesPercentage });

        }


    }
}
