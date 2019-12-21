namespace overapp.janus.Models.Domain
{
    public class Card : BaseCard
    {
        public string Number { get; set; }

        public string Cvv { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }
}