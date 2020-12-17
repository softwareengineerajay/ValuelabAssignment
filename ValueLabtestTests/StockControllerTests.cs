using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ValueLabtest.Controllers;
using ValueLabtest.Service;

namespace ValueLabtestTests
{
    class StockControllerTests
    {
        private Mock<IStockServices> stockservice;
        private StockController stockController;
        private static readonly string url = "https://financialmodelingprep.com/api/v3/quote/AAWW, AAL, CPAAW,PRAA, PAAS, RYAAY ?apikey=b351eb4b7226aefb40eb0da9df7cc616";

        [SetUp]
        public void Setup()
        {
            stockservice = new Mock<IStockServices>();
            stockController = new StockController(stockservice.Object);
        }
        [Test]
        public void GivenNullstockserviceStockController_ThenThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = new StockController(null));
        }

        [Test]
        public void GivenEmptyQuoteResultStockController_ThenReturnNotFound()
        {
            stockservice.Setup(x => x.GetQuoteDetailAsync(url)).ReturnsAsync(new List<ValueLabtest.Model.StockModel>());
            var response = stockController.GetStockItemsAsync();

            var notFoundResult = response.Result as NotFoundResult;

            Assert.IsTrue(response.IsCompletedSuccessfully);
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);

        }
        [Test]
        public void GivenNotEmptyQuoteResult_ThenReturnNotFound()
        {
            stockservice.Setup(x => x.GetQuoteDetailAsync(url)).ReturnsAsync(new List<ValueLabtest.Model.StockModel>() { new ValueLabtest.Model.StockModel() { } });
            var response = stockController.GetStockItemsAsync();
            var okResult = response.Result as OkObjectResult;

            Assert.IsTrue(response.IsCompletedSuccessfully);
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
    }
}
