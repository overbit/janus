using System;
using System.Net.Http;
using System.Threading.Tasks;
using overapp.janus.Models;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure.Services
{
    public class BankService : IBankService
    {
        private readonly HttpClient httpClient;

        public BankService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TransactionResult> ProcessPayment(Card card, BillingDetails billingDetails, double amount, string currencyCode)
        {
            throw new NotImplementedException();
        }
    }
}