using System;
using System.Linq;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Types;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace FwksLabs.Libs.AspNetCore.OpenApi;

public static class OpenApiDocumentConfiguration
{
    public static OpenApiOptions AddDocumentInfoTransformer(this OpenApiOptions options, OpenApiInfoOptions infoOptions, string version)
    {
        return options.AddDocumentTransformer((document, _, _) =>
        {
            document.Info.Title = infoOptions.Title;
            document.Info.Description = infoOptions.Description;
            document.Info.Version = version;
            document.Info.Contact = new OpenApiContact
            {
                Name = infoOptions.Owner,
                Email = $"teams.{infoOptions.Owner}@fwkslabs.com",
                Url = new Uri($"https://teams.fwkslabs.com/{infoOptions.Owner}")
            };

            return Task.CompletedTask;
        });
    }

    public static OpenApiOptions AddSecuritySchemeAndRequirement(this OpenApiOptions options, OpenApiSecurityScheme schema, params string[] scopes)
    {
        return options.AddDocumentTransformer((document, _, _) =>
        {
            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes[schema.Name] = schema;

            document.SecurityRequirements.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = schema.Name
                        }
                    },
                    scopes
                }
            });

            return Task.CompletedTask;
        });
    }

    public static OpenApiOptions AddJwtBearerSecurity(this OpenApiOptions options) =>
        options.AddSecuritySchemeAndRequirement(new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Name = AppSecuritySchemes.JwtBearer,
            Description = "JWT Authorization header using the Bearer scheme.",
            In = ParameterLocation.Header,
            Scheme = AppSecuritySchemes.JwtBearer.ToLower(),
            BearerFormat = "JWT"
        });

    public static OpenApiOptions AddOidcSecurity(this OpenApiOptions options, AuthServerOptions authServerOptions) =>
        options.AddSecuritySchemeAndRequirement(new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OpenIdConnect,
            Name = AppSecuritySchemes.Oidc,
            Description = "OIDC Authorization header using the Bearer scheme.",
            OpenIdConnectUrl = new Uri($"{authServerOptions.AuthorityUrl}/realms/{authServerOptions.Realm}/.well-known/openid-configuration")
        });

    public static OpenApiOptions AddOAuth2Security(this OpenApiOptions options, AuthServerOptions authServerOptions)
    {
        var baseUrl = $"{authServerOptions.AuthorityUrl}/realms/{authServerOptions.Realm}/protocol/openid-connect";

        return options.AddSecuritySchemeAndRequirement(new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Name = AppSecuritySchemes.OAuth2,
            Description = "OAuth2 Authorization Code Flow",
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{baseUrl}/auth"),
                    TokenUrl = new Uri($"{baseUrl}/token"),
                    RefreshUrl = new Uri($"{baseUrl}/refresh"),
                    Scopes = authServerOptions.Scopes
                }
            }
        });
    }

    public static OpenApiOptions AddCorrelationIdHeader(this OpenApiOptions options)
    {
        return options.AddDocumentTransformer((document, _, _) =>
        {
            foreach (var operation in document.Paths.SelectMany(path => path.Value.Operations.Values))
            {
                operation.Parameters ??= [];

                if (operation.Parameters.All(p => p.Name != AppHeaders.CorrelationId))
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = AppHeaders.CorrelationId,
                        In = ParameterLocation.Header,
                        Required = false,
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Format = "uuid",
                            Example = new OpenApiString(Guid.NewGuid().ToString())
                        },
                        Description = "Correlation Id for request tracking and tracing"
                    });
            }

            return Task.CompletedTask;
        });
    }

    public static OpenApiOptions AddSupportKeyHeader(this OpenApiOptions options, string route = "/v1/support/")
    {
        return options.AddDocumentTransformer((document, _, _) =>
        {
            foreach (var operation in document.Paths
                         .Where(x => x.Key.Contains(route, StringComparison.OrdinalIgnoreCase))
                         .SelectMany(x => x.Value.Operations.Values))
            {
                operation.Parameters ??= [];

                if (operation.Parameters.All(p => p.Name != AppHeaders.SupportKey))
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = AppHeaders.SupportKey,
                        In = ParameterLocation.Header,
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Example = new OpenApiString(Guid.NewGuid().ToString())
                        },
                        Description = "Support key for authorization"
                    });
            }

            return Task.CompletedTask;
        });
    }
}