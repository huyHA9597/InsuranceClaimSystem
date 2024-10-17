# InsuranceClaimSystem

## Purpose:

This project is an assignment trying to mimic a simplified version of an Insurance Claim System that manages customer claims.

The system should:
1. Allow customers to file claims online.
2. Store claims in a database.
3. Process claims and assign them a status (e.g., Pending, Approved, Rejected).
4. Provide an API to retrieve claims by their status.

## Technologies and patterns

This project repository is aspired based on [Vertical slice architecture in .NET 8 by Nadirbad](https://github.com/nadirbad/VerticalSliceArchitecture/tree/main).

- [ASP.NET API with .NET 8](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- CQRS with [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)
- [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
- [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Bogus](https://github.com/bchavez/Bogus)
- [MudBlazor](https://mudblazor.com/)

Afterwards, the projects and architecture is refactored towards the Vertical slice architecture style.

## Purpose of this repository

Since the timeline is only 4 days for 3 parts (API, Test and Web), I wanted to create a simpler API solution that focuses on the vertical slices architecture style.

Vertical slice architecture makes us stop thinking about horizontal layers and start thinking about vertical slices and organize code by **Features**. When the code is organized by feature you get the benefits of not having to jump around projects, folders and files. Things related to given features are placed close together.

When moving towards the vertical slices we stop thinking about layers and abstractions. The reason is the vertical slice doesn't necessarily need shared layer abstractions like repositories, services, controllers. We are more focused on concrete behavior implementation and what is the best solution to implements.

## Projects breakdown

The solution template is broken into 3 projects:

### Api

ASP.NET Web API project is an entry point to the application. It contains all the controller and domain logic for handling the claim management system.

#### Project structure:
- Database: To initialize and configure a DbContext instance. 
- Domain: Contain the **Claim** model
- Extensions: To contain the configuration for Database, CORS, validation and other services,...
- Features: Contain the controller and the claim endpoint logic. Each endpoint is splited into a separate folder. You can see that the logic handler and the validation will be owned for each endpoint. This make any newcomers who just seen the project can get used to understand the project quickly.

### Tests

Vertical slice architecture allow us to write the test easily for each layers. In the project, I had write a bunch of integration tests for the **Claim** endpoints. In each integration test, a clone of API will be create using WebApplicationFactory. Any requests that need to input personal information will be fake by **Bogus.Faker**.

### Web

This project's using ASP.NET Blazor Webassembly framework. I'm also using MudBlazor library to have better visualization on the component without much effort on CSS.

## Getting started

1. Install the latest [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Navigate to `InsuranceClaimSystem.API` and run `dotnet run` to launch the back end (ASP.NET Core Web API) or via `dotnet run --project .\InsuranceClaimSystem.API\InsuranceClaimSystem.API.csproj`
3. Open new powershell terminal, navigate to `InsuranceClaimSystem.Web` and run `dotnet run` to launch the front end (ASP.NET Blazor Webassembly app) or via `dotnet run --project .\InsuranceClaimSystem.Web\InsuranceClaimSystem.Web.csproj`
4. To run integration test, navigate to `InsuranceClaimSystem.Api.Tests` and run `dotnet run` to launch, or via `dotnet run --project .\InsuranceClaimSystem.API.Tests\InsuranceClaimSystem.API.Tests.csproj`

### Build, test and publish application

CLI commands executed from the root folder.

```bash
# build
dotnet build

# run
dotnet run --project .\InsuranceClaimSystem.API.\InsuranceClaimSystem.API.csproj

# launch website

dotnet run --project .\InsuranceClaimSystem.Web\InsuranceClaimSystem.Web.csproj

# run tests (required database up and running)
dotnet test tests/InsuranceClaimSystem.API.Tests/InsuranceClaimSystem.API.Tests.csproj 

# publish API
dotnet publish src/InsuranceClaimSystem.API/InsuranceClaimSystem.API.csproj --configuration Release 
```

### Test solution locally

To run API locally, for example to debug them, you can use the VS Code (just open and press F5) or other IDE (VisualStudio, Rider).
By default the project uses in-memory database.

To run the project from the terminal

```shell
dotnet run --project .\InsuranceClaimSystem.API.\InsuranceClaimSystem.API.csproj
```

and you'll be good to go everything will be up and running. Go the the indicated URL [http://localhost:5252/swagger](http://localhost:5252/swagger) and you'll see the API Swagger UI.

### Database Configuration

The project is configured to use an in-memory database by default.

## ToDo:

This project is simplified due to shortage of time. But in the end, there still be several things to be concerned and improve later.

- Validation and unit test for domain model.
- API authorization and authentication.
- Pagination for getAllClaims endpoint.


## Reference

- [Organize code by Feature using Vertical Slices by Derek Comartin](https://codeopinion.com/organizing-code-by-feature-using-vertical-slices/)
- [Vertical slice architecture by Milan Jovanovic](https://www.milanjovanovic.tech/blog/vertical-slice-architecture)
- [Vertical slice architecture in .NET 8 by Nadirbad](https://github.com/nadirbad/VerticalSliceArchitecture/tree/main)
