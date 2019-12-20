using Microsoft.EntityFrameworkCore;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure
{
    public class JanusContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public JanusContext() {}

        public JanusContext(DbContextOptions<JanusContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.MerchantId)
                .HasName("MerchantId");
        }
    }
}
