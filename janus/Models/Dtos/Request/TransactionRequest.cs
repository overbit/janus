using System.ComponentModel.DataAnnotations;

namespace overapp.janus.Models.Dtos.Request
{
    public class TransactionRequest
    {
        [Required]
        public Card Card { get; set; }

        [Required]
        public BillingDetails BillingDetails { get; set; }

        [Required]
        public double MyProperty { get; set; }

        [Required]
        public string CurrencyCode { get; set; }
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
        public string Clue { get; set; }

        [Required]
        public string Cvv { get; set; }

        [Required]
        public string ExpiryMonth { get; set; }

        [Required]
        public string ExpiryYear { get; set; }
    }
}