using System.Globalization;

namespace overapp.janus.Models.Domain
{
    public class Transaction
    {
        public string Id { get; set; }

        public string BankTransactionId { get; set; }

        public double Amount { get; set; }

        public RegionInfo BillingRegion { get; set; }

        public Card CardDetails { get; set; }

        public BillingDetails BillingDetails { get; set; }
    }
}