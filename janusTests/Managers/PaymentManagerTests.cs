using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using overapp.janus.Infrastructure.Repositories;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Models;
using overapp.janus.Models.Domain;
using overapp.janus.Models.Dtos.Request;
using BillingDetails = overapp.janus.Models.Dtos.Request.BillingDetails;
using Card = overapp.janus.Models.Dtos.Request.Card;

namespace overapp.janus.Managers.Tests
{
    [TestFixture]
    public class PaymentManagerTests
    {
        private Mock<IPaymentRepository> paymentRepoMock; 
        private Mock<IMerchantRepository> merchantRepoMock; 
        private Mock<IBankService> bankServiceMock;

        [SetUp]
        public void Setup()
        {
            paymentRepoMock = new Mock<IPaymentRepository>(MockBehavior.Strict);
            merchantRepoMock = new Mock<IMerchantRepository>(MockBehavior.Strict);
            bankServiceMock = new Mock<IBankService>(MockBehavior.Strict);
        }

        [Test]
        public void GetPaymentDetailsTest()
        {
            Assert.Fail();
        }

        //[Test()]
        //public void GetPaymentsPerMerchantTest()
        //{
        //    Assert.Fail();
        //}

        //[Test()]
        //public void GetPaymentsPerMerchantByDateTest()
        //{
        //    Assert.Fail();
        //}

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task ProcessPaymentTest(bool bankProcessedAsSuccess)
        {
            // Arrange 
            var clientId = "123";
            var req = new TransactionRequest()
            {
                Amount = 1005.3,
                CurrencyCode = "EUR",
                BillingDetails = new BillingDetails
                {
                    FirstName = "Massimo",
                    LastName = "Bottura",
                    AddressLine1 = "Osteria Francescana",
                    AddressLine2 = "Via Stella, 22",
                    City = "Modena",
                    Country = "Italy"
                },
                Card = new Card
                {
                    Number = "4111111111111111",
                    Cvv = "123",
                    ExpiryMonth = "11",
                    ExpiryYear = "25"
                }
            };

            merchantRepoMock.Setup(repository => repository.Get(It.IsAny<string>())).ReturnsAsync(new Merchant{ ClientId = clientId, Id = 1 });
            bankServiceMock.Setup(service => service.ProcessPayment(It.Is<Models.Domain.Card>(card => card.Clue == req.Card.Number &&
                                                                                                      card.Cvv == req.Card.Cvv &&
                                                                                                      card.ExpiryMonth == req.Card.ExpiryMonth &&
                                                                                                      card.ExpiryYear == req.Card.ExpiryYear), 
                                                                    It.Is<Models.Domain.BillingDetails>(details => details.FirstName == req.BillingDetails.FirstName &&
                                                                                                                   details.LastName == req.BillingDetails.LastName), 
                                                                    req.Amount, 
                                                                    req.CurrencyCode))
                            .ReturnsAsync(new TransactionResult { Id = "63ea10672f414485931862a49792699f" , Status = bankProcessedAsSuccess });

            var manager = new PaymentManager(paymentRepoMock.Object, merchantRepoMock.Object, bankServiceMock.Object);

            // Act 
            var result = await manager.ProcessPayment(clientId, req);

            // Assert
            Assert.IsNotNull(result.Guid);
            Assert.AreEqual(bankProcessedAsSuccess, result.IsSuccess);
        }
    }
}