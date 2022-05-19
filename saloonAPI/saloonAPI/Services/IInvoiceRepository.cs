using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IInvoiceRepository
    {
        public List<Invoice> GetAllInvoices();
        public Invoice SaveInvoice(Invoice invoice);
        public void UpdateInvoice(Invoice invoice);
        public void DeleteInvoice(Invoice invoice);
        public Invoice GetInvoice(int invoiceId);
        public List<Invoice> GetInvoiceByOrderId(int orderId);
        public Invoice GetInvoiceByOrder(int orderId);
        public Invoice GetInvoiceByAppoientment(int appointmentId);
    }
}
