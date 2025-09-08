using System.Collections.Generic;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public class ErrorCodeConfiguration
{
    private readonly Dictionary<int, int> _codes = new()
    {
        // General / Infrastructure
        [CommonErrors.Unknown] = StatusCodes.Status500InternalServerError,
        [CommonErrors.OperationFailed] = StatusCodes.Status500InternalServerError,
        [CommonErrors.InvalidState] = StatusCodes.Status400BadRequest,
        [CommonErrors.NotAllowed] = StatusCodes.Status403Forbidden,
        [CommonErrors.LimitExceeded] = StatusCodes.Status429TooManyRequests,
        [CommonErrors.Timeout] = StatusCodes.Status503ServiceUnavailable,
        [CommonErrors.DependencyUnavailable] = StatusCodes.Status503ServiceUnavailable,

        // Validation / Input
        [CommonErrors.Validation] = StatusCodes.Status400BadRequest,
        [CommonErrors.Required] = StatusCodes.Status400BadRequest,
        [CommonErrors.InvalidFormat] = StatusCodes.Status400BadRequest,
        [CommonErrors.OutOfRange] = StatusCodes.Status400BadRequest,
        [CommonErrors.Duplicate] = StatusCodes.Status409Conflict,
        [CommonErrors.NotFound] = StatusCodes.Status404NotFound,
        [CommonErrors.Conflict] = StatusCodes.Status409Conflict,

        // Security / Permissions
        [CommonErrors.Unauthorized] = StatusCodes.Status401Unauthorized,
        [CommonErrors.Forbidden] = StatusCodes.Status403Forbidden,

        // Concurrency / State
        [CommonErrors.AlreadyProcessed] = StatusCodes.Status409Conflict,
        [CommonErrors.Locked] = StatusCodes.Status423Locked,
        [CommonErrors.StaleData] = StatusCodes.Status409Conflict,

        // External / Integration
        [CommonErrors.UpstreamTimeout] = StatusCodes.Status504GatewayTimeout,
        [CommonErrors.BadGateway] = StatusCodes.Status502BadGateway,
        [CommonErrors.DependencyFailure] = StatusCodes.Status502BadGateway,
        [CommonErrors.ExternalServiceUnavailable] = StatusCodes.Status503ServiceUnavailable
    };

    public IDictionary<int, int> Codes => _codes;

    public static ErrorCodeConfiguration With(IDictionary<int, int> customErrors)
    {
        var codeConfiguration = new ErrorCodeConfiguration();

        foreach (var error in customErrors)
            codeConfiguration.Codes.TryAdd(error.Key, error.Value);

        return codeConfiguration;
    }
}