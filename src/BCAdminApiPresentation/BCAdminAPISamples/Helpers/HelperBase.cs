﻿using Microsoft.Dynamics.BusinessCentral.AdminCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCAdminAPISamples.Helpers
{
    internal class HelperBase(AdminCenterClient adminCenterClient)
    {
        protected AdminCenterClient _adminCenterClient = adminCenterClient;
    }
}
