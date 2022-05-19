using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IReport
    {
        public List<Appoinment> GetAppointmentsByYearRange(DateTime startDate, DateTime endDate);
        public List<Appoinment> GetAppointmentsByYear(int Year);
        public Invoice GetInvoiceForAppointment(int appointmentId);
        public List<ApplicationUser> GetUsersByYear(int year);
        public List<OrderDetail> GetOrderDetailsByYear(int year);
        public List<OrderInvoice> GetOrderInvoicesByOrder(int orderId);
    }
}
