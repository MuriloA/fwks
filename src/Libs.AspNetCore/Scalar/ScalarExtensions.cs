using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace FwksLabs.Libs.AspNetCore.Scalar;

public static class ScalarExtensions
{
    public static WebApplication MapDefaultScalar(this WebApplication app, string title, bool isDevelopment)
    {
        if (isDevelopment is false)
            return app;

        app.MapOpenApi();
        app.MapScalarApiReference(x =>
        {
            x.Title = title;
            x.Theme = ScalarTheme.Laserwave;
        });

        return app;
    }

    public static string[] ConfigureScalarDocuments(this IServiceCollection services, int[] versions)
    {
        var documents = versions.Select(v => new ScalarDocument($"v{v}", $"Version {v}"));

        services.Configure<ScalarOptions>(x => x.AddDocuments(documents));

        return [.. documents.Select(x => x.Name)];
    }
}