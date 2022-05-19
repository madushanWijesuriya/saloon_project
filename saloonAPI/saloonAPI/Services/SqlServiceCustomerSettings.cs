using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceCustomerSettings : ICustomerSettingsRepository
    {
        private readonly ApplicationDbContext _context = new ();
        public CustomerSetting SaveCustomerSettings(CustomerSetting customerSetting)
        {
            _context.CustomerSettings.Add(customerSetting);
            _context.SaveChanges();

            return _context.CustomerSettings.Find(customerSetting.Id);
        }

        public CustomerSetting GetCustomerSettings(string userId)
        {
            return _context.CustomerSettings.FirstOrDefault(c => c.UserId == userId);
        }

        public void UpdateCustomerSettings(CustomerSetting customerSetting)
        {
            _context.SaveChanges();
        }
    }
}
