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
        public Card Card { get; set; }
        
        [Required]
        public BillingDetails BillingDetails { get; set; }
    }

    public class BillingDetails
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }

    public class Card
    {
        [Required]
        [RegularExpression("[0-9]{16}")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}")]
        public string Cvv { get; set; }

        [Required]
        [RegularExpression("[0-9]{2}")]
        public string ExpiryMonth { get; set; }

        [Required]
        [RegularExpression("[0-9]{2}")]
        public string ExpiryYear { get; set; }
    }
}