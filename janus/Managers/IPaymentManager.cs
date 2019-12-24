using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Managers
{
    public interface IPaymentManager
    {
        Task<TransactionResultDto> ProcessPayment(string clientId, TransactionRequest request);

        Task<IEnumerable<TransactionDto>> GetPaymentsPerMerchant(string clientId, int? skip = null, int? take = null);

        //Task<IEnumerable<TransactionDto>> GetPaymentsPerMerchantByDate(string clientId, DateTime dateStart, DateTime dateEnd);

        Task<TransactionDto> GetPaymentDetails(string clientId, string paymentId);
    }
}