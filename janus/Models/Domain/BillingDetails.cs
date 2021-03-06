﻿using System.ComponentModel.DataAnnotations.Schema;

namespace overapp.janus.Models.Domain
{
    [Table("BillingAddresses", Schema = "JanusPaymentsSchema")]
    public class BillingDetails
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}