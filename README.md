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
