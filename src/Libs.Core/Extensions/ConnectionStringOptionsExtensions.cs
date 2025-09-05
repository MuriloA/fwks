using System;
using System.Linq;
using System.Web;
using FwksLabs.Libs.Core.Abstractions;
using Humanizer;

namespace FwksLabs.Libs.Core.Extensions;

public static class ConnectionStringOptionsExtensions
{
    private const string UserKey = "username";
    private const string PasswordKey = "password";
    private const string DbKey = "database";

    public static string BuildConnectionString(
        this IConnectionStringOptions builder, char separator, char aggregator, string server, Func<string, string> format)
    {
        return string.Join(separator, [
            server,
            .. builder.Options.Select(o => $"{format(o.Key)}{aggregator}{o.Value}")
        ]);
    }

    public static string BuildRedisConnectionString(this IConnectionStringOptions builder) => builder.BuildConnectionString(',', '=', builder.Server, InflectorExtensions.Camelize);

    public static string BuildPostgresConnectionString(this IConnectionStringOptions builder) =>
        builder.BuildConnectionString(';', '=', $"Host={builder.Server}", InflectorExtensions.Pascalize);

    public static string BuildLiteDbConnectionString(this IConnectionStringOptions builder) =>
        builder.BuildConnectionString(';', '=', $"Filename={builder.Server}", InflectorExtensions.Pascalize);

    public static string BuildMongoDbConnectionString(this IConnectionStringOptions builder, bool encodeUrl = false)
    {
        var username = GetValue(UserKey);
        var password = GetValue(PasswordKey);
        var database = GetValue(DbKey) ?? string.Empty;

        var credentials = username.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace() ? string.Empty : $"{username}:{password}@";

        var queryOptions = builder.Options
            .Where(o => new[] { UserKey, PasswordKey, DbKey }.Contains(o.Key) is false)
            .Select(o => $"{o.Key.Camelize()}={o.Value}");

        var options = queryOptions.ToList();

        var queryString = options.Count > 0 ? $"?{string.Join('&', options)}" : string.Empty;

        var connectionString = $"mongodb://{credentials}{builder.Server}/{database}{queryString}";

        return encodeUrl ? HttpUtility.UrlEncode(connectionString) : connectionString;

        string? GetValue(string key)
        {
            return builder.Options.TryGetValue(key, out var value) ? value?.ToString() : null;
        }
    }
}