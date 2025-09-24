using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Core.Configuration;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddValidationFactory(this IServiceCollection services) =>
        services.AddScoped<IValidationFactory, ValidationFactory>();
}