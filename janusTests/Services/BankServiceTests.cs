using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Mappers;

namespace overapp.janus.Services.Tests
{
    [TestFixture]
    public class BankServiceTests
    {
        [Test]
        public async Task ProcessPaymentTest()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{\"id\": \"4175075a-0944-4159-a094-617a3df4c611\", \"status\": true }")
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://therightdomain.com/"),
            };

            var config = new MapperConfiguration(opts => opts.AddProfile(new TransactionMapperProfile()));

            var service = new BankService(httpClient, config.CreateMapper());

            // Act
            var result = await service.ProcessPayment(new Models.Domain.Card(), new Models.Domain.BillingDetails(), 12, "GBP");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("4175075a-0944-4159-a094-617a3df4c611", result.Id);
            Assert.AreEqual(true, result.Status);

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Post
                  && req.RequestUri == new Uri("https://therightdomain.com/process/payment")
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}