using System;

namespace Domain.Entities;

public class Order
{
    public int OrderId { get; set; }              // รหัสการสั่งซื้อ (Primary Key)
    public string CustomerName { get; set; }      // ชื่อลูกค้า
    public DateTime OrderDate { get; set; }       // วันที่สั่งซื้อ
    public decimal TotalAmount { get; set; }      // ยอดรวมของการสั่งซื้อ
                                                  // เพิ่ม Property อื่น ๆ ตามที่ต้องการ
}
