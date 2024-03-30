using Microsoft.Dynamics.BusinessCentral.AdminCenter;
using Microsoft.Dynamics.BusinessCentral.AdminCenter.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BCAdminAPISamples.Helpers
{
    internal class SetAppInsightsKeyHelper : HelperBase
    {
        public SetAppInsightsKeyHelper(AdminCenterClient adminCenterClient) 
            : base(adminCenterClient)
        { }
        public async Task SetAppInsightsKeyAsync(string appInsightsKey, string environmentName)
        {
            var applicationInsights = new ApplicationInsights
            {
                Key = appInsightsKey
            };
            await _adminCenterClient.SetApplicationInsightsInstrumentationKeyAsync(
                "BusinessCentral", //ApplicationType
                environmentName, //Environment Name
                applicationInsights);//ConnectionString
            Console.WriteLine("App Insights Key Set");
        }
    }
}
