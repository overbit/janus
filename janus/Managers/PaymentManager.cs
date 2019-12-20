﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using overapp.janus.Infrastructure.Repositories;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Mappers;
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
            var card = TransactionMapper.MapToDomainCard(request.Card);
            var billingDetails = TransactionMapper.MapToDomainBillingDetails(request.BillingDetails);
            
            var bankResponseTask = bankService.ProcessPayment(card, billingDetails, request.Amount, request.CurrencyCode);

            var merchantIdTask = merchantRepository.Get(clientId);

            var bankResponse = await bankResponseTask;
            var merchant = await merchantIdTask;

            var transaction = new Transaction
            {
                Amount = request.Amount,
                CurrencyCode = request.CurrencyCode,
                BankTransactionId = bankResponse.Id,
                Guid = Guid.NewGuid().ToString("N"),
                MerchantId = merchant.Id,
                BillingDetails = billingDetails,
                CardDetails = card
            };

            return new TransactionResult
            {
                Guid = transaction.Guid,
                IsSuccess = bankResponse.Status
            };
        }
    }
}
