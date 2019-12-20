using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Managers
{
    public interface IPaymentManager
    {
        Task<TransactionResult> ProcessPayment(string clientId, TransactionRequest request);

        Task<IEnumerable<TransactionResult>> GetPaymentsPerMerchant(string clientId, int? skip = null, int? take = null);
        
        Task<IEnumerable<TransactionResult>> GetPaymentsPerMerchantByDate(string clientId, DateTime dateStart, DateTime dateEnd);

        Task<TransactionDetails> GetPaymentDetails(string clientId, Guid paymentGuid);
    }
}