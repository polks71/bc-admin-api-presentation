using Microsoft.Dynamics.BusinessCentral.AdminCenter;
using Microsoft.Dynamics.BusinessCentral.AdminCenter.Models;
using Newtonsoft.Json;
using Environment = Microsoft.Dynamics.BusinessCentral.AdminCenter.Models.Environment;

namespace BCAdminAPISamples.Helpers
{
    internal class EnvironmentHelper : HelperBase
    {
        public EnvironmentHelper(AdminCenterClient adminCenterClient) :
            base(adminCenterClient)
        { }

        public async Task<List<Environment>> GetEnvironmentsAsync()
        {
            var returnEnvironments = new List<Environment>();
            var enviornmentListReturn = await _adminCenterClient.GetEnvironmentsAsync();
            if (enviornmentListReturn != null && enviornmentListReturn.Value != null)
            {
                Console.WriteLine($"{enviornmentListReturn.Value.Value.Count} Environments Returned");
                var environmentList = enviornmentListReturn.Value.Value;
                foreach (var environment in environmentList)
                {
                    Console.WriteLine($"EnvironmentId:{environment.Name} EnvironmentType = {environment.Type}");
                    returnEnvironments.Add(environment);
                }
            }
            return returnEnvironments;
        }

        public async Task CreateEnvironmentAsync(string environmentType, string countryCode,
            string name, 
            string ring, 
            string version )
        {
            var environments = await GetEnvironmentsAsync();
            //Validate if a production environment already exists.
            //Only one production environment can exist, by default.
            if (environmentType == "Production" &&
                environments.Any(el => el.Type == "Production"))
            {
                Console.WriteLine("Production environment already exists");
                return;
            }
            //Check if there are already three sandbox environments.
            //Only allowed three by default
            else if (environmentType == "Sandbox" &&
                environments.Count(el => el.Type == "Sandbox") >= 3)
            {
                Console.WriteLine("Three sandbox environments already exist");
                return;
            }


            var createEnvironmentRequest = new CreateEnvironmentRequest()
            {
                EnvironmentType = environmentType,
                CountryCode = countryCode,
            };
            if (!string.IsNullOrWhiteSpace(version))
            {
                createEnvironmentRequest.ApplicationVersion = version;
            }
            if (!string.IsNullOrWhiteSpace(ring))
            {
                createEnvironmentRequest.RingName = ring;
            }

            Console.WriteLine("Environment Create");
            var environmentOperation = await _adminCenterClient.CreateEnvironmentAsync("BusinessCentral", name, createEnvironmentRequest);
            Console.WriteLine($"EnvironmentOperation:{JsonConvert.SerializeObject(environmentOperation.Value, Formatting.Indented)}");
            Console.WriteLine("Environment Started");

            var whileExit = false;
            while (!whileExit)
            {
                var operations = await _adminCenterClient.GetOperationsForAllEnvironmentsAsync();
                if (operations.HasValue && operations.Value.Value.Count > 0
                    && operations.Value.Value.Any(op => op.Id == environmentOperation.Value.Id))
                {
                    //using first. Any already proved one exists
                    var operation = operations.Value.Value.First(op => op.Id == environmentOperation.Value.Id);
                    if (operation.Status.HasValue)
                    {
                        if (operation.Status == EnvironmentOperationStatus.Queued ||
                            operation.Status == EnvironmentOperationStatus.Scheduled ||
                            operation.Status == EnvironmentOperationStatus.Running)
                        {
                            Console.WriteLine($"Create Still Running with Status {operation.Status}");
                        }
                        else if (operation.Status == EnvironmentOperationStatus.Succeeded ||
                            operation.Status == EnvironmentOperationStatus.Failed ||
                            operation.Status == EnvironmentOperationStatus.Canceled ||
                            operation.Status == EnvironmentOperationStatus.Skipped)
                        { 
                            Console.WriteLine($"Create Completed with Status {operation.Status}");
                            whileExit = true;
                        }
                        
                    }
                    if (!whileExit)
                    {
                        Console.WriteLine("Sleeping for 60 seconds");
                        Thread.Sleep(60000);//sleep one minute
                    }
                }
                else
                {
                    whileExit = true;
                    Console.WriteLine("No Operations Returned, or OperationId Not Found");
                } 
            }
        }
    }
}
