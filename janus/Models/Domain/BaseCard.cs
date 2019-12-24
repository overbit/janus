using System.ComponentModel.DataAnnotations.Schema;

namespace overapp.janus.Models.Domain
{
    [Table("CreditCards", Schema = "JanusPaymentsSchema")]
    public class BaseCard
    {
        public int Id { get; set; }

        public string Clue { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }
}