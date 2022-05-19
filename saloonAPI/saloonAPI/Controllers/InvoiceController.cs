using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saloonAPI.Services;
using saloonAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _sqlService; IAppointmentRepository appointmentRepository;
        private readonly IAppointmentRepository _sqlServiceAppoinment;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository dataAccessRepository, IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _sqlServiceAppoinment = appointmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Invoice>> GetAllinvoices()
        {
            List<Invoice> invoices = _sqlService.GetAllInvoices();
            var vmInvoices = _mapper.Map<List<Invoice>>(invoices);
            return Ok(vmInvoices);
        }
        [HttpPost, Authorize]
        public ActionResult<Invoice> CreateInvoice(Invoice invoice)
        {

            var selectedAppoinment = _sqlServiceAppoinment.GetAppoinment(invoice.AppoinmentId.Value);
           
            if(_sqlService.GetInvoiceByAppoientment(selectedAppoinment.Id) is not null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Appointment already has an invoice!" });
            }
            if (selectedAppoinment is not null && selectedAppoinment.Status == AppoinmentStatus.Completed)
            {
                var r = new Random();
                //invoice no generate 
                invoice.InvoiceNo = "Inv-" + invoice.Id + "-" + invoice.CreatedDate.ToString("yyyy-MM-dd") + r.Next(0, 100000).ToString();
                _sqlService.UpdateInvoice(invoice);
                double chargePerMin = 0;
                double totalCharge = 0;
                invoice.CreatedDate = DateTime.Now;
                //calculate invoice amount
                int noOfMins = selectedAppoinment.DurationInMins;


                if (selectedAppoinment.Type == AppoinmentType.HairCut)
                {
                    chargePerMin = 40;
                    totalCharge = chargePerMin * noOfMins;

                }
                else if (selectedAppoinment.Type == AppoinmentType.ColouringAndStyling)
                {
                    chargePerMin = 35;
                    totalCharge = chargePerMin * noOfMins;
                }
                else if (selectedAppoinment.Type == AppoinmentType.FacialAndSkinCareTreatment)
                {
                    chargePerMin = 30;
                    totalCharge = chargePerMin * noOfMins;
                }
                else if (selectedAppoinment.Type == AppoinmentType.Makeup)
                {
                    chargePerMin = 25;
                    totalCharge = chargePerMin * noOfMins;
                }
                else if (selectedAppoinment.Type == AppoinmentType.NailTreatment)
                {
                    chargePerMin = 20;
                    totalCharge = chargePerMin * noOfMins;
                }
                else if (selectedAppoinment.Type == AppoinmentType.WaxingAndHairRemoval)
                {
                    chargePerMin = 15;
                    totalCharge = chargePerMin * noOfMins;
                }
                else
                {
                    chargePerMin = 10;
                    totalCharge = chargePerMin * noOfMins;
                }

                invoice.Amount = totalCharge;
                var result = _sqlService.SaveInvoice(invoice);

                if (result is not null)
                {
                    selectedAppoinment.Status = AppoinmentStatus.Paid;
                    _sqlServiceAppoinment.UpdateAppoinment(selectedAppoinment);

                    return Created("Invoice", invoice);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Something went wrong!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Please complete the appointment before make invoice!" });
        }
        [HttpGet("order/{orderId}")]
        public ActionResult<Invoice> GetInvoiceByOrder(int orderId)
        {
           Invoice invoice = _sqlService.GetInvoiceByOrder(orderId);
            return Ok(invoice);
        }
        [HttpGet("appointment/{appointmentId}")]
        public ActionResult<Invoice> GetInvoiceByAppoinement(int appointmentId)
        {
            Invoice invoice = _sqlService.GetInvoiceByAppoientment(appointmentId);
            return Ok(invoice);
        }

        [HttpGet("getAllInvoiceForProductOrder/{orderId}"), Authorize]
        public IActionResult GetAllInvoiceForProductOrder(int orderId)
        {
            var invoices = _sqlService.GetInvoiceByOrderId(orderId);
            return Ok(invoices);
        }
    }

}
