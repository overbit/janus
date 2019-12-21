using System.ComponentModel.DataAnnotations;

namespace overapp.janus.Models.Dtos.Request
{
    public class CardDto
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