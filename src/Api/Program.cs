using Api.Security;
using Api.Swagger;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Microsoft.Identity.Web, which provides the glue between ASP.NET Core, the authentication middleware, and the Microsoft Authentication Library (MSAL) for .NET.
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection(SecurityOptions.Key));

builder.Services.AddControllers();

builder.Services.AddSwaggerCustom(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerCustom(app.Configuration);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    // add the default policy to all endpoints
    .RequireAuthorization();

app.Run();
