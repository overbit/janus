using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories.Tests
{
    [TestFixture]
    public class PaymentRepositoryTests
    {
        [Test]
        public async Task GetTest()
        {
            var options = new DbContextOptionsBuilder<JanusContext>()
                .UseInMemoryDatabase(databaseName: "get-transaction")
                .Options;

            using (var context = new JanusContext(options))
            {
                context.Add(new Transaction { Id = 1, Amount = 11.45, ExternalId = "externalId", Status = true, MerchantId = 1});
                context.Add(new Transaction { Id = 2, Amount = 0.55, ExternalId = "externalId2", Status = false, MerchantId = 1 });
                context.SaveChanges();
            }

            using (var newContext = new JanusContext(options))
            {
                var paymentRepo = new PaymentRepository(newContext);
                var result = await paymentRepo.Get("externalId", 1);

                Assert.AreEqual(1, result.Id);
                Assert.AreEqual(11.45, result.Amount);
                Assert.IsTrue(result.Status);
            }
        }

        [Test]
        public async Task AddTest()
        {
            var options = new DbContextOptionsBuilder<JanusContext>()
                .UseInMemoryDatabase(databaseName: "save-a-transaction")
                .Options;

            using (var context = new JanusContext(options))
            {
                var paymentRepo = new PaymentRepository(context);
                await paymentRepo.Add(new Transaction { Id = 1, Amount = 11.45, ExternalId = "externalId", Status = true });
                await paymentRepo.Add(new Transaction { Id = 2, Amount = 11.45, ExternalId = "externalId2", Status = true });
                await paymentRepo.Add(new Transaction { Id = 3, Amount = 11.45, ExternalId = "externalId3", Status = true });
            }

            using (var newContext = new JanusContext(options))
            {
                var transactions = await newContext.Transactions.ToListAsync();

                Assert.AreEqual(3, transactions.Count);
            }
        }
    }
}