using System;
using System.Collections.Generic;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Cors;

public static class CorsExtensions
{
    public static IServiceCollection AddAllowAnyPolicy(this IServiceCollection services)
    {
        return services
            .AddCors(options => options
                .AddDefaultPolicy(builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()));
    }

    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IEnumerable<CorsPolicyOptions> policies)
    {
        var defaultIsAdded = false;

        return services.AddCors(options =>
        {
            foreach (var policy in policies)
            {
                if (policy.Name.EqualsTo("default"))
                {
                    if (defaultIsAdded)
                        throw new InvalidOperationException("Only one default policy is allowed.");

                    options.AddDefaultPolicy(BuildPolicy(policy));
                    defaultIsAdded = true;
                }
                else
                    options.AddPolicy(policy.Name, BuildPolicy(policy));
            }
        });

        Action<CorsPolicyBuilder> BuildPolicy(CorsPolicyOptions policy)
        {
            return options =>
            {
                options
                    .WithHeaders(policy.AllowedHeaders)
                    .WithMethods(policy.AllowedMethods)
                    .WithOrigins(policy.AllowedOrigins)
                    .WithExposedHeaders(policy.ExposedHeaders)
                    .SetPreflightMaxAge(TimeSpan.FromMinutes(policy.PreflightMaxAgeSeconds));

                if (policy.AllowCredentials)
                    options.AllowCredentials();
            };
        }
    }
}