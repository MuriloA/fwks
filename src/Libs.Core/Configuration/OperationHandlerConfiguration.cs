using System;
using System.Linq;
using FwksLabs.Libs.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Core.Configuration;

public static class OperationHandlerConfiguration
{
    private static readonly Type[] HandlerTypes =
    {
        typeof(IOperationHandler<>),
        typeof(IOperationHandler<,>)
    };

    public static IServiceCollection AddOperationHandlersFromAssembly<TAssembly>(this IServiceCollection services)
    {
        var assembly = typeof(TAssembly).Assembly;

        foreach (var type in assembly.GetTypes())
        {
            if (type is { IsAbstract: true, IsInterface: true })
                continue;

            var handlers = type.GetInterfaces().Where(x =>
                x.IsGenericType &&
                HandlerTypes.Any(handlerType => handlerType == x.GetGenericTypeDefinition()));

            foreach (var handlerType in handlers)
                services.AddScoped(handlerType, type);
        }

        return services;
    }
}