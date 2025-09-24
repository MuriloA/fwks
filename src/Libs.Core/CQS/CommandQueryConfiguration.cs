using System;
using System.Linq;
using FwksLabs.Libs.Core.CQS.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Core.CQS;

public static class CommandQueryConfiguration
{
    private static readonly Type[] HandlerTypes =
    [
        typeof(ICommandHandler<>),
        typeof(ICommandHandler<,>),
        typeof(IQueryHandler<,>)
    ];

    public static IServiceCollection AddHandlersFromAssembly<TAssembly>(this IServiceCollection services)
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