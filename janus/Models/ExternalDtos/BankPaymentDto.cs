using System.Text.Json.Serialization;

namespace overapp.janus.Models.ExternalDtos
{
    public class BankPaymentDto
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("card")]
        public BankPaymentCardDto CardDetails { get; set; }

        [JsonPropertyName("billing-details")]
        public BankPaymentBillingDetailsDto BillingDetails { get; set; }
    }
}
