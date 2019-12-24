using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using overapp.janus.Models;
using overapp.janus.Models.Domain;
using overapp.janus.Models.ExternalDtos;

namespace overapp.janus.Infrastructure.Services
{
    public class BankService : IBankService
    {
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        public BankService(HttpClient httpClient, IMapper mapper)
        {
            this.httpClient = httpClient;
            this.mapper = mapper;
        }

        public async Task<TransactionResult> ProcessPayment(Card card, BillingDetails billingDetails, double amount, string currencyCode)
        {
            var endpoint = "/process/payment";

            var bankPaymentDto = new BankPaymentDto
            {
                Amount = amount,
                Currency = currencyCode,
                CardDetails = mapper.Map<BankPaymentCardDto>(card),
                BillingDetails = mapper.Map<BankPaymentBillingDetailsDto>(billingDetails)
            };

            var body = JsonSerializer.Serialize(bankPaymentDto);

            var response = await httpClient.PostAsync(endpoint, new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TransactionResult>(content);
            }
            else
            {
                throw new System.Exception("Bank API is experiencing issues");
            }
        }
    }
}