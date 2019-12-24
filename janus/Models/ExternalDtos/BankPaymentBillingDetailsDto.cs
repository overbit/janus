using System.Text.Json.Serialization;

namespace overapp.janus.Models.ExternalDtos
{
    public class BankPaymentBillingDetailsDto
    {
        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last-name")]
        public string LastName { get; set; }

        [JsonPropertyName("address-line")]
        public string AddressLine { get; set; }

        [JsonPropertyName("address-city")]
        public string City { get; set; }

        [JsonPropertyName("address-country")]
        public string Country { get; set; }
    }
}
