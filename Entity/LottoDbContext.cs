using Microsoft.EntityFrameworkCore;

namespace Lotto.Entity
{
    public class LottoDbContext : DbContext
    {
      public LottoDbContext(DbContextOptions<LottoDbContext> options) : base(options)
       {

       }
        public DbSet<Player> Players { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                 .HasOne(p => p.Ticket)
                 .WithOne(t => t.Player)
                 .HasForeignKey<Ticket>(t => t.PlayerId);
        }
    }

    
}
