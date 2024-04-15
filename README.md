# Business Central Admin API Session
This is the source code for the Business Central Admin API session presented at:
- Direction NA in April 2024
- DynamicsCon in May 2024

# Projects
Two projects in the solution src\BCAdminApiPresentation\BCAdminApiPresentation.sln
## Application Landing
This is a simple ASP.Net website that uses an Entra ID App Registration for authentication. The Azure Tenant needs to have the Entra ID App either as a Registered App or an Enterprise App. The only purpose of this website is to provide a redirect URL for admin consent requests.

## BC Admin API Samples
This is a small console application that provides some samples using the [BC Admin API SDK](https://www.nuget.org/packages/Microsoft.Dynamics.BusinessCentral.AdminCenter) nuget package.

## Admin Center Authentication
Authentication of the Admin Center Client using the Entra ID Tenant ID, Entra ID App Id, and app secret. This approach allows for a multi-tenant application to authentication in any customer tenant where admin consent has been granted. 

## References
[Admin Center API](https://learn.microsoft.com/en-us/dynamics365/business-central/dev-itpro/administration/administration-center-api)

[BC Tech Github Repo](https://github.com/microsoft/BCTech/tree/master/samples/AdminCenterApi)

[BC Telemetry](https://learn.microsoft.com/en-us/dynamics365/business-central/dev-itpro/administration/telemetry-overview)

[Entra ID Admin Consent](https://learn.microsoft.com/en-us/entra/identity/enterprise-apps/user-admin-consent-overview)

[Custom Page Connectors](https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/connections-list#standard-and-custom-connectors)

![image](https://github.com/TruNorth-Dynamics/bc-admin-api-presentation/assets/23003226/ba378d62-dce8-4585-86b2-5360ba80207c)

