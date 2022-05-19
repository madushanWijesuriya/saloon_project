 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime CreatedDate { get; set; }
        [Range(0, Double.MaxValue)]
        public double Amount { get; set; }
        public int? AppoinmentId { get; set; }
        public virtual Appoinment Appoinment { get; set; }
        public virtual ICollection<OrderInvoice> OrderInvoices { get; set; } = new List<OrderInvoice>();
    }
}
