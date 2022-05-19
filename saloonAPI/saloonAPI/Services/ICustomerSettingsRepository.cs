using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public interface ICustomerSettingsRepository
    {
        public CustomerSetting SaveCustomerSettings(CustomerSetting customerSetting);
        public CustomerSetting GetCustomerSettings(string userId);
        public void UpdateCustomerSettings(CustomerSetting customerSetting);
    }
}
