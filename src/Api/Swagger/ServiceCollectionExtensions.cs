using Api.Security;
using Microsoft.OpenApi.Models;

namespace Api.Swagger;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerCustom(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var securityOptions = GetSecurityOptions(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swaggerOptions =>
        {
            swaggerOptions.CustomSchemaIds(y => y.FullName!.Replace("+", "."));
            swaggerOptions.SupportNonNullableReferenceTypes();
            swaggerOptions.EnableAnnotations();
            if (!string.IsNullOrEmpty(securityOptions.Instance))
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(
                                new Uri(securityOptions.Instance),
                                $"{securityOptions.TenantId}/oauth2/v2.0/authorize"
                            ),
                            TokenUrl = new Uri(
                                new Uri(securityOptions.Instance),
                                $"{securityOptions.TenantId}/oauth2/v2.0/token"
                            ),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"{securityOptions.Audience}/{Scope.AccessAsUser}", "" },
                            },
                        },
                    },
                };
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2",
                            },
                        },
                        new[] { $"{securityOptions.Audience}/{Scope.AccessAsUser}" }
                    },
                };
                swaggerOptions.AddSecurityDefinition("oauth2", securityScheme);
                swaggerOptions.AddSecurityRequirement(securityRequirement);
            }
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerCustom(
        this IApplicationBuilder app,
        IConfiguration configuration
    )
    {
        var securityOptions = GetSecurityOptions(configuration);
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.OAuthClientId(securityOptions.SwaggerClientId);
            c.DisplayOperationId();
            c.OAuthScopes($"{securityOptions.Audience}/{Scope.AccessAsUser}");
            c.OAuthUsePkce();
        });
        return app;
    }

    private static SecurityOptions GetSecurityOptions(IConfiguration configuration) =>
        configuration.GetSection(SecurityOptions.Key).Get<SecurityOptions>()
        ?? new SecurityOptions();
}
