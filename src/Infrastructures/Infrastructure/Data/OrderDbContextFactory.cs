using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    // Design-time factory สำหรับ OrderDbContext
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            // ระบุ connection string ที่ต้องการใช้ในช่วง design-time
            //optionsBuilder.UseSqlServer("data source=localhost;initial catalog=Order;user id=sa;password=P@ssw@rd;TrustServerCertificate=True;multipleactiveresultsets=True;");
            optionsBuilder.UseSqlServer("Data Source=10.112.85.37;Initial Catalog=Order;Persist Security Info=True;User ID=dev1;Password=p@ssw0rd;Connect Timeout=10000;MultipleActiveResultSets=True;");

            return new OrderDbContext(optionsBuilder.Options);
        }
    }
}