using Microsoft.Dynamics.BusinessCentral.AdminCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCAdminAPISamples.Helpers
{
    internal class HelperBase
    {
        protected AdminCenterClient _adminCenterClient;
        public HelperBase(AdminCenterClient adminCenterClient) 
        {
            _adminCenterClient = adminCenterClient;
        }
    }
}
