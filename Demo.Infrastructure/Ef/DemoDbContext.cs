using Demo.Infrastructure.Ef.Model;
using Microsoft.EntityFrameworkCore;


namespace Demo.Infrastructure.Ef
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Demo");
        }

        public DbSet<UserDb> Users { get; set; }
        public DbSet<TicketBookDb> TicketBooks { get; set; }
        public DbSet<TicketDb> Tickets { get; set; }
        public DbSet<ControllerDb> Controllers { get; set; }
    }
}