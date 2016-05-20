namespace DataAccess
{
    using System.Data.Entity;
    using Domain.Entities;

    public class SalesReportDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}
