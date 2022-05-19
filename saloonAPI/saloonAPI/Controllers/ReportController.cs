using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport _sqlService;
        public ReportController(IReport dataAccessService)
        {
            _sqlService = dataAccessService;
        }

        [HttpPost("appointsByType"), Authorize]
        public IActionResult AppointmentsByType(ReportsInputDates postData)
        {
            List<Appoinment> appoinments = _sqlService.GetAppointmentsByYearRange(postData.StartDate, postData.EndDate);
            List<AppointmentByTypeVM> results = appoinments
                .GroupBy(a => a.Type)
                .Select(i => new AppointmentByTypeVM
                {
                    Type = i.First().Type,
                    TypeText = i.First().Type.ToString(),
                    Count = i.Count()
                }).ToList();
            return Ok(results);
        }

        [HttpPost("appointmentAmountsByYear"), Authorize]
        public IActionResult AppointmentAmountsByYear(ReportsInputYear postData)
        {
            List<Appoinment> appoinments = _sqlService.GetAppointmentsByYear(postData.Year);
            List<AppointmentAmountsByYearVM> results = appoinments
                .GroupBy(a => a.CreatedDate.Month)
                .Select(i => new AppointmentAmountsByYearVM
                {
                   Count = i.Count(),
                   Month = i.First().CreatedDate.Month,
                   MonthText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i.First().CreatedDate.Month),
                   Amount = i.Sum(c =>
                   {
                       Invoice invoiceForAppointment = _sqlService.GetInvoiceForAppointment(c.Id);
                       if (invoiceForAppointment is null) return 0;
                       return invoiceForAppointment.Amount;
                   })
                }).ToList();

            return Ok(results);
        }

        [HttpPost("customersAppointmentCounts"), Authorize]
        public IActionResult CustomersAppointmentCounts(ReportsInputYear postData)
        {
            List<Appoinment> appoinments = _sqlService.GetAppointmentsByYear(postData.Year);
            List<UserAppointmentCountsVM> results = appoinments
                .GroupBy(a => a.User.Email)
                .Select(i => new UserAppointmentCountsVM
                {
                    AppointmentCount = i.Count(),
                    Email = i.First().User.Email,
                    JoinDate = i.First().User.JoinDate,
                    JoinDateFormatted = i.First().User.JoinDate.ToString("yyyy-MM-dd")
                }).ToList();

            return Ok(results);
        }

        [HttpPost("howManyUsersPerMonth"), Authorize]
        public IActionResult HowManyUsersPerMonth(ReportsInputYear postData)
        {
            List<ApplicationUser> users = _sqlService.GetUsersByYear(postData.Year);
            List<HowManyUsersPerMonth> results = users
                .GroupBy(a => a.JoinDate.Month)
                .Select(i => new HowManyUsersPerMonth
                {
                   UsersCount = i.Count(),
                   Month = i.First().JoinDate.Month,
                   MonthText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i.First().JoinDate.Month)
                }).ToList();

            return Ok(results);
        }

        [HttpPost("howManyProductSoldByMonth"), Authorize]
        public IActionResult HowManyProductSoldByMonth(ReportsInputYear postData)
        {
            List<OrderDetail> orderDetails = _sqlService.GetOrderDetailsByYear(postData.Year);
            List<HowManyProductsSoldByMonthVM> results = orderDetails
                .GroupBy(a => a.CreatedDate.Month)
                .Select(i => new HowManyProductsSoldByMonthVM
                {
                    ProductsSoldCount = i.Count(),
                    Month = i.First().CreatedDate.Month,
                    MonthText = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i.First().CreatedDate.Month),
                    ProductsSoldAmount = i.Sum(s =>
                    {
                        List<OrderInvoice> orderInvoices = _sqlService.GetOrderInvoicesByOrder(s.OrderId);
                        if (orderInvoices is null || orderInvoices.Count() < 1) return 0;
                        return orderInvoices.Sum(oi => oi.Invoice.Amount);
                    })
                }).ToList();

            return Ok(results);
        }
    }
}
