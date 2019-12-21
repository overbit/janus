using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using overapp.janus.Infrastructure.Repositories;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Models.Domain;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

namespace overapp.janus.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMerchantRepository merchantRepository;
        private readonly IBankService bankService;
        private ILogger<PaymentManager> logger;
        private readonly IMapper mapper;

        public PaymentManager(IPaymentRepository paymentRepository,
                                IMerchantRepository merchantRepository,
                                IBankService bankService,
                                ILogger<PaymentManager> logger,
                                IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.merchantRepository = merchantRepository;
            this.bankService = bankService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<TransactionDto> GetPaymentDetails(string clientId, Guid paymentGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionDto>> GetPaymentsPerMerchant(string clientId, int? skip, int? take)
        {
            var merchant = await merchantRepository.Get(clientId);

            var transactions = await paymentRepository.GetTransactionsByMerchant(merchant.Id);

            return transactions.Select(t => mapper.Map<TransactionDto>(t));
        }

        //public async Task<IEnumerable<TransactionResult>> GetPaymentsPerMerchantByDate(string clientId, DateTime dateStart, DateTime dateEnd)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<TransactionResultDto> ProcessPayment(string clientId, TransactionRequest request)
        {
            var card = mapper.Map<Card>(request.Card);
            var billingDetails = mapper.Map<BillingDetails>(request.BillingDetails);

            var bankResponseTask = bankService.ProcessPayment(card, billingDetails, request.Amount, request.CurrencyCode);

            var merchantIdTask = merchantRepository.Get(clientId);

            var bankResponse = await bankResponseTask;
            var merchant = await merchantIdTask;

            var transaction = new Transaction
            {
                Amount = request.Amount,
                CurrencyCode = request.CurrencyCode,
                BankTransactionId = bankResponse.Id,
                ExternalId = Guid.NewGuid().ToString("N"),
                MerchantId = merchant.Id,
                BillingDetails = billingDetails,
                CardDetails = card
            };

            try
            {
                await paymentRepository.Add(transaction);
            }
            catch (Exception )
            {
                logger.LogError("Unable to store transaction: ", JsonSerializer.Serialize(transaction));
                throw;
            }

            return new TransactionResultDto
            {
                Guid = transaction.ExternalId,
                IsSuccess = bankResponse.Status
            };
        }

        
    }
}
