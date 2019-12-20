using System.ComponentModel.DataAnnotations.Schema;

namespace overapp.janus.Models.Domain
{
    [Table("Transactions", Schema = "JanusPaymentsSchema")]
    public class Transaction
    {
        public string Id { get; set; }

        public string Guid { get; set; }

        public string BankTransactionId { get; set; }

        public double Amount { get; set; }

        public string CurrencyCode { get; set; }

        public Card CardDetails { get; set; }

        public BillingDetails BillingDetails { get; set; }

        public int MerchantId { get; set; }
    }
}