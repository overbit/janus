using overapp.janus.Models.Dtos.Request;

namespace overapp.janus.Models.Dtos.Response
{
    public class TransactionDto
    {
        public string Id { get; set; }

        public bool IsSuccess { get; set; }

        public double Amount { get; set; }

        public string CurrencyCode { get; set; }

        public string CardClue { get; set; }

        public BillingDetailsDto BillingDetails { get; set; }
    }
}