namespace DataAccess
{
    using System.Data.Entity;
    using Domain;

    public class SalesReportDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
