using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Infrastructure.Data;

public class OrderDbContext: DbContext, IOrderDbContext
{
        // Constructor รับ DbContextOptions
        public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options) { }

        // DbSet สำหรับ Order
        public DbSet<Order> Orders { get; set; }

        // กำหนดการเชื่อมต่อกับ SQL Server (ถ้ายังไม่ได้กำหนดใน Startup)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // แก้ไข connection string ให้ตรงกับการตั้งค่าของคุณ
                optionsBuilder.UseSqlServer("Server=localhost;initial catalog=DataCleanArchitecture;user id=sa;password=P@ssw@rd;TrustServerCertificate=True;multipleactiveresultsets=True;");
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
 