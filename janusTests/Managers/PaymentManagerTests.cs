﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using overapp.janus.Infrastructure.Repositories;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Mappers;
using overapp.janus.Models;
using overapp.janus.Models.Domain;
using overapp.janus.Models.Dtos.Request;

namespace overapp.janus.Managers.Tests
{
    [TestFixture]
    public class PaymentManagerTests
    {
        private Mock<IPaymentRepository> paymentRepoMock;
        private Mock<IMerchantRepository> merchantRepoMock;
        private Mock<IBankService> bankServiceMock;
        private IMapper mapper;
        private Mock<ILogger<PaymentManager>> loggerMock;
        private PaymentManager manager;

        [SetUp]
        public void Setup()
        {
            paymentRepoMock = new Mock<IPaymentRepository>(MockBehavior.Strict);
            merchantRepoMock = new Mock<IMerchantRepository>(MockBehavior.Strict);
            bankServiceMock = new Mock<IBankService>(MockBehavior.Strict);
            loggerMock = new Mock<ILogger<PaymentManager>>();

            var config = new MapperConfiguration(opts => opts.AddProfile(new TransactionMapperProfile()));
            mapper = config.CreateMapper();

            manager = new PaymentManager(paymentRepoMock.Object, merchantRepoMock.Object, bankServiceMock.Object, loggerMock.Object, mapper);
        }

        [Test]
        public async Task GetPaymentDetailsTest()
        {
            var clientId = "1a34";
            var transactionId = "12313123";
            var merchantId = 11;

            merchantRepoMock.Setup(repo => repo.Get(clientId)).ReturnsAsync(new Merchant { ClientId = clientId, Id = merchantId });
            paymentRepoMock.Setup(repo => repo.Get(transactionId, merchantId)).ReturnsAsync(new Transaction { MerchantId = merchantId, ExternalId = transactionId, Amount = 202.1, Status = true });

            var result = await manager.GetPaymentDetails(clientId, transactionId);

            Assert.AreEqual(transactionId, result.Id);
            Assert.IsTrue(result.IsSuccess);
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
                BillingDetails = new BillingDetailsDto
                {
                    FirstName = "Massimo",
                    LastName = "Bottura",
                    AddressLine1 = "Osteria Francescana",
                    AddressLine2 = "Via Stella, 22",
                    City = "Modena",
                    Country = "Italy"
                },
                Card = new CardDto
                {
                    Number = "4111111111111111",
                    Cvv = "123",
                    ExpiryMonth = "11",
                    ExpiryYear = "25"
                }
            };

            merchantRepoMock.Setup(repository => repository.Get(It.IsAny<string>())).ReturnsAsync(new Merchant { ClientId = clientId, Id = 1 });
            bankServiceMock.Setup(service => service.ProcessPayment(It.Is<Card>(card => card.Number == req.Card.Number &&
                                                                                                      card.Cvv == req.Card.Cvv &&
                                                                                                      card.ExpiryMonth == req.Card.ExpiryMonth &&
                                                                                                      card.ExpiryYear == req.Card.ExpiryYear),
                                                                    It.Is<BillingDetails>(details => details.FirstName == req.BillingDetails.FirstName &&
                                                                                                                   details.LastName == req.BillingDetails.LastName),
                                                                    req.Amount,
                                                                    req.CurrencyCode))
                            .ReturnsAsync(new TransactionResult { Id = "63ea10672f414485931862a49792699f", Status = bankProcessedAsSuccess });

            paymentRepoMock.Setup(repository => repository.Add(It.Is<Transaction>(transaction =>
                transaction.Amount.Equals(req.Amount) &&
                transaction.CurrencyCode == req.CurrencyCode))).Returns(Task.CompletedTask);


            // Act 
            var result = await manager.ProcessPayment(clientId, req);

            // Assert
            Assert.IsNotNull(result.Guid);
            Assert.AreEqual(bankProcessedAsSuccess, result.IsSuccess);
        }
    }
}