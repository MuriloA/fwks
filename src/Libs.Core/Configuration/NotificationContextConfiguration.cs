using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Core.Configuration;

public static class NotificationContextConfiguration
{
    public static IServiceCollection AddNotificationContext(this IServiceCollection services) =>
        services
            .AddScoped<INotificationContext, NotificationContext>();

    public static IServiceCollection AddNotificationContext<TNotificationContext>(this IServiceCollection services)
        where TNotificationContext : class, INotificationContext =>
        services
            .AddScoped<INotificationContext, TNotificationContext>();
}