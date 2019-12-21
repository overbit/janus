using System.ComponentModel.DataAnnotations;
using overapp.janus.Attributes;

namespace overapp.janus.Models.Dtos.Request
{
    public class TransactionRequest
    {

        [Required]
        public double Amount { get; set; }

        [Required]
        [StringIsoCurrencyCode]
        public string CurrencyCode { get; set; }

        [Required]
        public CardDto Card { get; set; }
        
        [Required]
        public BillingDetailsDto BillingDetails { get; set; }
    }
}