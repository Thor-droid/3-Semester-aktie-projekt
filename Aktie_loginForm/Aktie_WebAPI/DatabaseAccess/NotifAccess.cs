using Aktie_WebAPI.Model;
using Microsoft.EntityFrameworkCore;
using Aktie_WebAPI.Model;

namespace Aktie_WebAPI.DatabaseAccess
{
    public class NotifAccess : DbContext
    {
        public NotifAccess(DbContextOptions<NotifAccess> options)
       : base(options)
        {
        }

        public DbSet<PriceAlert> PriceAlerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceAlert>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StockSymbol)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TargetPrice)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IsTriggered)
                    .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
