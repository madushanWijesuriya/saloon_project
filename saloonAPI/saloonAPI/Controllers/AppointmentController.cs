using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using saloonAPI.Services;
using saloonAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _sqlService;
        private readonly IInvoiceRepository _sqlServiceInvoice;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentRepository dataAccessRepository, IInvoiceRepository dataAccessRepositoryInvoice, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _sqlServiceInvoice = dataAccessRepositoryInvoice;
            _mapper = mapper;
        }

        [HttpGet, Authorize]
        public ActionResult<IList<Appoinment>> GetAllAppointments()
        {
            List<Appoinment> appoinments = _sqlService.GetAllAppointment();
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            appoinments = appoinments.Where(a => a.UserId == UserId).ToList();
            var vmAppoinment = _mapper.Map<List<Appoinment>>(appoinments);
            return Ok(vmAppoinment);
        }
       
        [HttpPost, Authorize]
        public ActionResult<Appoinment> CreateAppointment(Appoinment appoinment)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            appoinment.CreatedDate = DateTime.Now;
            appoinment.UserId = UserId;
            _sqlService.SaveAppoinment(appoinment);
           
            return Created("Appoinment", appoinment);
        }

        [HttpPut("{appointmentId}"), Authorize]
        public IActionResult UpdateAppointment(int appointmentId, Appoinment appoinment)
        {
            var appoinmentSelected = _sqlService.GetAppoinment(appointmentId);

            if (appoinmentSelected is null)
            {
                return NotFound();
            }
            else if (appoinmentSelected.Status != AppoinmentStatus.Created)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "This appointment cannot update!" });
            }

            appoinmentSelected.Type = appoinment.Type;
            appoinmentSelected.DurationInMins = appoinment.DurationInMins;
            appoinmentSelected.AppoinmentDate = appoinment.AppoinmentDate;
            appoinmentSelected.CreatedDate = appoinment.CreatedDate;

            _sqlService.UpdateAppoinment(appoinmentSelected);

            return NoContent();
        }

        [HttpPut("{appointmentId}/status-update"), Authorize]
        public IActionResult StatusUpdateAppointment(int appointmentId, Appoinment appoinment)
        {
            var appoinmentSelected = _sqlService.GetAppoinment(appointmentId);
           
            if (appoinmentSelected is null)
            {
                return NotFound();
            }
            else if (_sqlServiceInvoice.GetInvoiceByAppoientment(appoinmentSelected.Id) is not null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Cannot Change this Appointment!" });
            }
            else if (appoinmentSelected.Status == AppoinmentStatus.Completed)
            {
                appoinmentSelected.CompletedDate = DateTime.Now;
            }
            else if (appoinment.Status == AppoinmentStatus.Paid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Cannot Paid Appointment!" });
            }


            appoinmentSelected.Status = appoinment.Status;
            _sqlService.UpdateAppoinment(appoinmentSelected);

            return NoContent();
        }

        [HttpDelete("{appoinmentId}"), Authorize]
        public IActionResult DeleteAppoinment(int appoinmentId)
        {
            var selectedAppoinment = _sqlService.GetAppoinment(appoinmentId);
            if (selectedAppoinment is null)
            {
                return NotFound();
            }

            _sqlService.DeleteAppoinment(selectedAppoinment);

            return NoContent();
        }
    }

}
