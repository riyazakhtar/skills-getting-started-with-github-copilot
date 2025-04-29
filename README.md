# C# Clean Architecture Solution Template

A template for C# .NET API solutions. The template uses **Clean Architecture** approach for organizing code.

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)
- [Docker](https://www.docker.com) or [Podman](https://github.com/containers/podman)

All commands must be executed from `src` directory.

Run PostgreSQL locally:

```sh
docker compose up -d
```

Restore [.NET tools](https://learn.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-local-tool):

```sh
dotnet tool restore
```

Update database: 

```sh
dotnet ef database update -s Api
```

Run: 

```sh
dotnet run --project ./Api
```

Open the application url: http://127.0.0.1:5131/swagger/index.html

## Database

The template is configured to use PostgreSQL.

To update database use:

```
cd src
dotnet ef database update -s Api
```

To seed the database use you need to toggle `seed` app setting when updating the database:

```
# as a command-line argument
dotnet ef database update -s Api -- --seed true

# or as an environment variables
seed=true dotnet ef database update -s Api
```

Data seeding is implemented using `UseSeeding` and `UseAsyncSeeding` methods of Entity Framework Core. Test data located in `src/Infrastructure/Data/TestDataSeeder.cs`. 

To create a new migration use the following command:

```sh
cd src
dotnet ef migrations add AddProduct -p Infrastructure -s Api
```

To renove a new migration use the following command (be careful with `-f` flag):

```sh
cd src
dotnet ef migrations remove -p Infrastructure -s Api -f
```

## Authentication and authorization

Authentication is the process of determining a user's identity. Authorization is the process of determining whether a user has access to a resource.

Basic principals are described [here](https://inter-ikea.atlassian.net/wiki/spaces/CBRA/pages/929398876/IAM+-+Authentication+Authorization).

For authentication and authorization [Microsoft.Identity.Web](https://learn.microsoft.com/en-us/entra/msal/dotnet/microsoft-identity-web/) library is used. The library simplifies integration with the Microsoft identity platform.

### Api App registration

You must have app Registration for your application or domain. App registration can be requested [here](https://interikea.service-now.com/help?id=sc_cat_item&sys_id=0bbedd301b14b1d09d132024b24bcbec).

Copy client id and audience to appsettings.json (AzureAd:ClientId and AzureAd:Audience fields).

Create required [app roles](https://learn.microsoft.com/en-us/entra/identity-platform/howto-add-app-roles-in-apps) (set Allowed member types to Both) and put them into `Api/Security/Role.cs` file.

Assign user groups or users to those app roles.

For On-Behalf-Of flow create a scope named `access_as_user`.

### Swagger UI App Registration

You must have app Registration for Swagger UI. App registration can be requested [here](https://interikea.service-now.com/help?id=sc_cat_item&sys_id=0bbedd301b14b1d09d132024b24bcbec).

Copy client id to appsettings.json (AzureAd:SwaggerClientId field).

Add delegated permission for `access_as_user` scope exposed by Api App registration.

Add Single-page application platform configuration to Swagger UI App Registration with redirect url like this `https://localhost/swagger/oauth2-redirect.html`

## Solution structure

```sh
├── 📁 Api # Application entry point, contains no business logic.
│   ├── Controllers
│   ├── Security
│   ├── Swagger
│   ├── Api.csproj
│   ├── ControllerBaseExtensions.cs
│   ├── Dockerfile
│   ├── Program.cs
│   ├── appsettings.Development.json
│   └── appsettings.json
│
├── 📁 Application # Coordinates application operations using the CQRS pattern.
│   ├── Common
│   ├── Products
│   ├── Application.csproj
│   └── DependencyInjection.cs
│
├── 📁 Domain # Encapsulates core business logic and entities, independent of external dependencies.
│   ├── Common
│   ├── Entities
│   ├── Results
│   └── Domain.csproj
│
├── 📁 Infrastructure # Manages external system interactions, implementing interfaces from the Domain and Application layers.
│   ├── Data
│   ├── Migrations
│   ├── DependencyInjection.cs
│   └── Infrastructure.csproj
│
└── DotnetTemplate.sln
```

## Technologies

1. [MediatR](https://github.com/jbogard/MediatR). Mediator implementation in .NET.
1. [EF Core](https://learn.microsoft.com/en-us/ef/core/). ORM.
1. [Npgsql.EntityFrameworkCore.PostgreSQL](https://github.com/npgsql/efcore.pg). Entity Framework Core provider for PostgreSQL.
1. [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore). Swagger tools for documenting API.
1. [AutoMapper](https://github.com/AutoMapper/AutoMapper). Convention-based object-object mapper in .NET

## Learn more

1. https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
1. https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-sdk-templates
1. https://github.com/ardalis/CleanArchitecture
1. https://github.com/jasontaylordev/CleanArchitecture

## Support

If you are having problems, please let me know by raising a new issue.
