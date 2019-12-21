using System.ComponentModel.DataAnnotations;

namespace overapp.janus.Models.Dtos.Request
{
    public class BillingDetailsDto
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
}