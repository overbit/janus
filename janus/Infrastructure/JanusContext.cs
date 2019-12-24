using Microsoft.EntityFrameworkCore;
using overapp.janus.Models.Domain;

namespace overapp.janus.Infrastructure
{
    public class JanusContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BaseCard> Cards { get; set; }
        public DbSet<BillingDetails> BillingDetails { get; set; }
        public DbSet<Merchant> Merchants { get; set; }

        public JanusContext() {}

        public JanusContext(DbContextOptions<JanusContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(te =>
                {
                    te.HasOne(o => o.CardDetails)
                        .WithOne()
                        .HasForeignKey<BaseCard>(bc => bc.Id);
                    te.HasOne(o => o.BillingDetails)
                        .WithOne()
                        .HasForeignKey<BillingDetails>(b => b.Id);
                    te.HasIndex(t => t.MerchantId)
                        .HasName("MerchantId");
                });
        }
    }
}
