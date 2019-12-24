namespace overapp.janus.Models.Domain
{
    public class BaseCard
    {
        public int Id { get; set; }

        public string Clue { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }
    }
}