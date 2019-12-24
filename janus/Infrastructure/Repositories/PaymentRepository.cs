using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly JanusContext context;

        public PaymentRepository(JanusContext context)
        {
            this.context = context;
        }

        public async Task<Transaction> Get(string paymentExternalId, int merchantId)
        {
            return await context.Transactions.Where(t => t.ExternalId == paymentExternalId &&
                                                        t.MerchantId == merchantId)
                                             .FirstAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByMerchant(int merchantId)
        {
            return await context.Transactions.Where(t => t.MerchantId == merchantId).ToListAsync();
        }

        public async Task Add(Transaction transaction)
        {
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
        }
    }
}