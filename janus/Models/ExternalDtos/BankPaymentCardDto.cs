using System.Text.Json.Serialization;

namespace overapp.janus.Models.ExternalDtos
{
    public class BankPaymentCardDto
    {
        [JsonPropertyName("clue")]
        public string Number { get; set; }

        [JsonPropertyName("cvv")]
        public string Cvv { get; set; }

        [JsonPropertyName("exp-month")]
        public string ExpiryMonth { get; set; }

        [JsonPropertyName("exp-year")]
        public string ExpiryYear { get; set; }
    }
}
