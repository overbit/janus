using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories.Tests
{
    [TestFixture]
    public class MerchantRepositoryTests
    {
        [Test]
        public async Task GetTest()
        {
            var options = new DbContextOptionsBuilder<JanusContext>()
                .UseInMemoryDatabase(databaseName: "merchantDb")
                .Options;


            using (var context = new JanusContext(options))
            {
                context.Add(new Merchant { Id = 1, Name = "MerchantA", ClientId = "123", ClientSecret = "Megasecret" });
                context.SaveChanges();
            }

            using (var newContext = new JanusContext(options))
            {
                var merchantrepo = new MerchantRepository(newContext);
                var result = await merchantrepo.Get("123");

                Assert.AreEqual(1, result.Id);
                Assert.AreEqual("MerchantA", result.Name);
                Assert.AreEqual("123", result.ClientId);
                Assert.AreEqual("Megasecret", result.ClientSecret);
            }
        }
    }
}