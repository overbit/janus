using System.Threading.Tasks;
using overapp.janus.Models;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Services
{
    public interface IBankService
    {
        Task<TransactionResult> ProcessPayment(Card card, BillingDetails billingDetails, double amount, string currencyCode);
    }
}
