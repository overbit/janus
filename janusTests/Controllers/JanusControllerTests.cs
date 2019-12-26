using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using overapp.janus.Managers;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Controllers.Tests
{
    [TestFixture]
    public class JanusControllerTests
    {
        Mock<IPaymentManager> paymentManager;

        [SetUp]
        public void Setup()
        {
            paymentManager = new Mock<IPaymentManager>(MockBehavior.Strict);
        }

        [Test]
        public async Task GetTransactionReturns400WhenInvalidIdTest()
        {
            var paymentId = "invalidguid";
            var controller = new JanusController(paymentManager.Object);

            var result = await controller.GetTransaction("123", null, paymentId);

            Assert.AreEqual(typeof(BadRequestResult), result.Result.GetType());
        }

        [Test]
        public async Task GetTransactionReturns404WhenNoTransactionIsFoundTest()
        {
            var paymentId = Guid.NewGuid().ToString("N");
            var controller = new JanusController(paymentManager.Object);

            paymentManager.Setup(manager => manager.GetPaymentDetails("123", paymentId)).ReturnsAsync((TransactionDto)null);

            var result = await controller.GetTransaction("123", null, paymentId);

            Assert.AreEqual(typeof(NotFoundResult), result.Result.GetType());
        }

        [Test]
        public async Task GetTransactionTest()
        {
            var paymentId = Guid.NewGuid().ToString("N");
            var controller = new JanusController(paymentManager.Object);

            paymentManager.Setup(manager => manager.GetPaymentDetails("123", paymentId)).ReturnsAsync(new TransactionDto
            {
                Id = paymentId
            });

            var result = await controller.GetTransaction("123", null, paymentId);

            Assert.AreEqual(paymentId, result.Value.Id);
        }

        [Test]
        public async Task ProcessTransactionTest()
        {
            paymentManager.Setup(manager => manager.ProcessPayment("123", It.IsAny<TransactionRequest>())).ReturnsAsync(new TransactionResultDto());
            var controller = new JanusController(paymentManager.Object);

            var result = await controller.ProcessTransaction("123", null, new TransactionRequest());

            Assert.IsNotNull(result.Value);
        }
    }
}