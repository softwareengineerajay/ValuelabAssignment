using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValueLabtest;
using ValueLabtest.Model;
using ValueLabtest.Service;

namespace ValueLabtestTests
{
    class StockServicesTest
    {
        private StockServices stockServices;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private Mock<IHttpClientFactory> httpclientFactory;
        [SetUp]
        public void Setup()
        {
       
            httpclientFactory = new Mock<IHttpClientFactory>();

            stockServices = new StockServices( httpclientFactory.Object); ;

           
        }
        [Test]
        public void GivenNullHttpHandlerStockservice_ThenThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new StockServices(null));
        }
        [Test]
        public void GivenValidHttpHandlerStockservice_ThenThrowDoNotException()
        {
            var url = "https://financialmodelingprep.com/api/v3/quote/AAWW, AAL, CPAAW,PRAA, PAAS, RYAAY ?apikey=b351eb4b7226aefb40eb0da9df7cc616";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'symbol':'AAL','name':'AmericanAirlinesGroupInc.','price':16.86,'changesPercentage':-0.88,'change':-0.15,'dayLow':16.58,'dayHigh':17.0384,'yearHigh':30.78,'yearLow':8.25,'marketCap':10297650176,'priceAvg50':13.745143,'priceAvg200':13.275971,'volume':58052072,'avgVolume':75618181,'exchange':'NASDAQ','open':16.88,'previousClose':17.01,'eps':-14.003,'pe':null,'earningsAnnouncement':'2020-10-22T07:00:10.000+0000','sharesOutstanding':610774032,'timestamp':1608160548}]"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            httpclientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act & Assert
            Assert.DoesNotThrowAsync(() => stockServices.GetQuoteDetailAsync(url));
        }
        [Test]
        public void GivenInvalidHttpHandlerStockservice_ThenThrowArgumentNullException()
        {
            var url = "";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'symbol':'AAL','name':'AmericanAirlinesGroupInc.','price':16.86,'changesPercentage':-0.88,'change':-0.15,'dayLow':16.58,'dayHigh':17.0384,'yearHigh':30.78,'yearLow':8.25,'marketCap':10297650176,'priceAvg50':13.745143,'priceAvg200':13.275971,'volume':58052072,'avgVolume':75618181,'exchange':'NASDAQ','open':16.88,'previousClose':17.01,'eps':-14.003,'pe':null,'earningsAnnouncement':'2020-10-22T07:00:10.000+0000','sharesOutstanding':610774032,'timestamp':1608160548}]"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            httpclientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => stockServices.GetQuoteDetailAsync(url));
        }
        [Test]
        public void GivenNotOKStatusCodeHttpHandlerStockservice_ThenThrowDoNotException()
        {
            var url = "https://financialmodelingprep.com/api/v3/quote/AAWW, AAL, CPAAW,PRAA, PAAS, RYAAY ?apikey=b351eb4b7226aefb40eb0da9df7cc616";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                   
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpclientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            // Act
            var stockModels = stockServices.GetQuoteDetailAsync(url);

            //Assert
            Assert.IsNull(stockModels.Result);
        }
    }
}
