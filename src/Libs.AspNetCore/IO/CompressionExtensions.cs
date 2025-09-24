using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.IO;

public static class CompressionExtensions
{
    public static IServiceCollection AddDefaultResponseCompression(this IServiceCollection services) =>
        services
            .AddResponseCompression()
            .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
            .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
            .Configure<ResponseCompressionOptions>(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat([MediaTypeNames.Application.Json, MediaTypeNames.Application.ProblemJson]);
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });
}