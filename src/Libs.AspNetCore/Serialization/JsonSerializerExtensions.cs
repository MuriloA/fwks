using FwksLabs.Libs.Core.Configuration;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FwksLabs.Libs.AspNetCore.Serialization;

public static class JsonSerializerExtensions
{
    public static IHostApplicationBuilder ConfigureJsonSerializer(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<JsonOptions>(options =>
            JsonSerializerOptionsConfiguration.Configure(options.SerializerOptions));

        return builder;
    }
}