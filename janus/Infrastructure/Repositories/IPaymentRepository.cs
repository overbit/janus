using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<Transaction> Get(string paymentExternalId);

        Task<IEnumerable<Transaction>> GetTransactionsByMerchant(int merchantId);

        Task Add(Transaction transaction);
    }
}