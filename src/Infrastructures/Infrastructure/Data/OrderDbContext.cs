using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Infrastructure.Data;

public class OrderDbContext : DbContext, IOrderDbContext
{
    // Constructor รับ DbContextOptions
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    // DbSet สำหรับ Order
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderCompleted> OrderCompleteds { get; set; }

    // กำหนดการเชื่อมต่อกับ SQL Server (ถ้ายังไม่ได้กำหนดใน Startup)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // แก้ไข connection string ให้ตรงกับการตั้งค่าของคุณ
            //optionsBuilder.UseSqlServer("Server=localhost;initial catalog=DataCleanArchitecture;user id=sa;password=P@ssw@rd;TrustServerCertificate=True;multipleactiveresultsets=True;");
            optionsBuilder.UseSqlServer("Data Source=10.112.85.37;Initial Catalog=Order;Persist Security Info=True;User ID=dev1;Password=p@ssw0rd;Connect Timeout=10000;MultipleActiveResultSets=True; TrustServerCertificate=True; Encrypt=False;");
           
        }
    }

    // กำหนดการตั้งค่าของ entity เพิ่มเติม (Optional)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);          
            entity.Property(e => e.CustomerName)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.OrderDate)
                  .HasDefaultValueSql("GETDATE()");
            entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<OrderCompleted>().HasOne(o => o.Order)
                                             .WithMany()
                                             .HasForeignKey(o => o.OrderId);
    }
}
