using System;

namespace overapp.janus.Models.Domain
{
    public class Merchant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // TODO Store as Guid.ToString("N")
        public string ClientId { get; set; }
        
        // This will live in a Secret Management system
        public string ClientSecret { get; set; }
    }
}
