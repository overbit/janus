using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Managers
{
    public class PaymentManager : IPaymentManager
    {
        public async Task<TransactionDetails> GetPaymentDetails(string clientId, Guid paymentGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionResult>> GetPaymentsPerMerchant(string clientId, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionResult>> GetPaymentsPerMerchantByDate(string clientId, DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionResult> ProcessPayment(string clientId, TransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
