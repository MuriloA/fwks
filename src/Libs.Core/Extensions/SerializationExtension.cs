using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Configuration;

namespace FwksLabs.Libs.Core.Extensions;

public static class SerializationExtension
{
    public static string Serialize(this object source, Action<JsonSerializerOptions>? optionsAction = null) => JsonSerializer.Serialize(source, BuildOptions(optionsAction));

    public static Task SerializeAsync<T>(this Stream source, T target, Action<JsonSerializerOptions>? optionsAction = null, CancellationToken cancellationToken = default) =>
        JsonSerializer.SerializeAsync(source, target, BuildOptions(optionsAction), cancellationToken);

    public static T Deserialize<T>(this string source, Action<JsonSerializerOptions>? optionsAction = null) => JsonSerializer.Deserialize<T>(source, BuildOptions(optionsAction))!;

    public static ValueTask<T> DeserializeAsync<T>(this Stream source, Action<JsonSerializerOptions>? optionsAction = null, CancellationToken cancellationToken = default) =>
        JsonSerializer.DeserializeAsync<T>(source, BuildOptions(optionsAction), cancellationToken)!;

    private static JsonSerializerOptions BuildOptions(Action<JsonSerializerOptions>? optionsAction)
    {
        var options = JsonSerializerOptionsConfiguration.Default();

        optionsAction?.Invoke(options);

        return options;
    }
}