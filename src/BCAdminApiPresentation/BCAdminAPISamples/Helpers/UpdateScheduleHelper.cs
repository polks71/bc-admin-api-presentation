using Microsoft.Dynamics.BusinessCentral.AdminCenter;
using Microsoft.Dynamics.BusinessCentral.AdminCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCAdminAPISamples.Helpers
{
    internal class UpdateScheduleHelper : HelperBase
    {
        public UpdateScheduleHelper(AdminCenterClient adminCenterClient)
            : base(adminCenterClient)
        { }

        public async Task<ScheduledUpgrade> GetScheduledUpdatesAsync(string environmentName)
        {
            var upgradeResponse = await _adminCenterClient.GetScheduledUpgradeAsync("BusinessCentral", environmentName);
            if (upgradeResponse != null && upgradeResponse.Value != null)
            {
                var upgrade = upgradeResponse.Value;
                return upgrade;
            }
            else
                return default(ScheduledUpgrade);
        }

        public async Task ScheduleUpdateAsync(string environmentName, DateTimeOffset dateTimeOffset, bool ignoreUpradeWindow = false)
        {
            var scheduledUpgrade = await GetScheduledUpdatesAsync(environmentName);
            if (scheduledUpgrade != default(ScheduledUpgrade))
            {
                Console.WriteLine("Before Change Settings");
                Console.WriteLine($"Environment Name: {scheduledUpgrade.EnvironmentName}");
                Console.WriteLine($"Upgrade Status: {scheduledUpgrade.UpgradeStatus}");
                Console.WriteLine($"Upgrade Scheduled: {scheduledUpgrade.UpgradeDate}");
                Console.WriteLine($"Source Version: {scheduledUpgrade.SourceVersion}");
            }
            var reschedule = new RescheduleUpgradeRequest() 
            { 
                IgnoreUpgradeWindow = ignoreUpradeWindow,
                RunOn = dateTimeOffset
            };
            await _adminCenterClient.SetScheduledUpgradeAsync("BusinessCentral", environmentName, reschedule);
            scheduledUpgrade = await GetScheduledUpdatesAsync(environmentName);
            if (scheduledUpgrade != default(ScheduledUpgrade))
            {
                Console.WriteLine("After Change Settings");
                Console.WriteLine($"Environment Name: {scheduledUpgrade.EnvironmentName}");
                Console.WriteLine($"Upgrade Status: {scheduledUpgrade.UpgradeStatus}");
                Console.WriteLine($"Upgrade Scheduled: {scheduledUpgrade.UpgradeDate}");
                Console.WriteLine($"Source Version: {scheduledUpgrade.SourceVersion}");
            }
        }
    }
}
