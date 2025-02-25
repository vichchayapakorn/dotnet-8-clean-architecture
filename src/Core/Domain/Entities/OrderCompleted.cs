using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class OrderCompleted
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Status { get; set; }

        // Navigation property (if needed)
        public virtual Order Order { get; set; }
    }
}