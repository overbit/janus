using System.Text.Json.Serialization;

namespace overapp.janus.Models
{
    public class TransactionResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}