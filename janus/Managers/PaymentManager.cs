using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Infrastructure.Repositories;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMerchantRepository merchantRepository;
        private readonly IBankService bankService;

        public PaymentManager(IPaymentRepository paymentRepository, IMerchantRepository merchantRepository, IBankService bankService)
        {
            this.paymentRepository = paymentRepository;
            this.merchantRepository = merchantRepository;
            this.bankService = bankService;
        }

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
