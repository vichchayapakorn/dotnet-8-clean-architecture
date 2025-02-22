using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderDbContext: DbContext
    {
        // Constructor รับ DbContextOptions
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        // DbSet สำหรับ Order
        public DbSet<Order> Orders { get; set; }

        // กำหนดการเชื่อมต่อกับ SQL Server (ถ้ายังไม่ได้กำหนดใน Startup)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // แก้ไข connection string ให้ตรงกับการตั้งค่าของคุณ
                optionsBuilder.UseSqlServer("Server=ocalhost;initial catalog=DataCleanArchitecture;user id=sa;password=P@ssw@rd;TrustServerCertificate=True;multipleactiveresultsets=True;");
            }
           
        }

        // กำหนดการตั้งค่าของ entity เพิ่มเติม (Optional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);  // กำหนด Primary Key

                // กำหนดความยาวและความจำเป็นของฟิลด์ CustomerName
                entity.Property(e => e.CustomerName)
                      .IsRequired()
                      .HasMaxLength(100);

                // กำหนดค่าเริ่มต้นสำหรับ OrderDate เป็นวันที่ปัจจุบัน
                entity.Property(e => e.OrderDate)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }

    public class Order
    {
        public int OrderId { get; set; }              // รหัสการสั่งซื้อ (Primary Key)
        public string CustomerName { get; set; }      // ชื่อลูกค้า
        public DateTime OrderDate { get; set; }       // วันที่สั่งซื้อ
        public decimal TotalAmount { get; set; }      // ยอดรวมของการสั่งซื้อ
        // เพิ่ม Property อื่น ๆ ตามที่ต้องการ
    }