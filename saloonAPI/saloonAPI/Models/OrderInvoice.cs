using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Models
{
    public class OrderInvoice
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
