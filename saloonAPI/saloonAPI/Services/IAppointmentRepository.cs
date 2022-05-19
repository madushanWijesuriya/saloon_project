using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface IAppointmentRepository
    {
        public List<Appoinment> GetAllAppointment();
        public Appoinment SaveAppoinment(Appoinment appoinment);
        public void UpdateAppoinment(Appoinment appoinment);    
        public void DeleteAppoinment(Appoinment appoinment);
        public Appoinment GetAppoinment(int appoinmentId);
        public void UpdateStatus(Appoinment appoinment, int status);
    }
}
