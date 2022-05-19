using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceAppointment : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public List<Appoinment> GetAllAppointment()
        {
            return _context.Appoinments.ToList();
        }
        public Appoinment GetAppoinment(int appoinmentId)
        {
            return _context.Appoinments.FirstOrDefault(c => c.Id == appoinmentId);
        }

        public Appoinment SaveAppoinment(Appoinment appoinment)
        {
            _context.Appoinments.Add(appoinment);
            _context.SaveChanges();

            return _context.Appoinments.Find(appoinment.Id);
        }

        public void UpdateAppoinment(Appoinment appoinment)
        {
            _context.SaveChanges();
        }
        public void DeleteAppoinment(Appoinment appoinment)
        {
            _context.Appoinments.Remove(appoinment);
            _context.SaveChanges();
        }
        public void UpdateStatus(Appoinment appoinment, int status)
        {
            appoinment.Status = (AppoinmentStatus)status;
            _context.SaveChanges();
        }
    }
}
