using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Constants;

public class CommonErrors
{
    // 0x000 – 0x0FF : General / Infrastructure
    public static readonly ApplicationError Unknown =
        new(0x001, nameof(Unknown), "Internal Server Error", "An unexpected error occurred.");

    public static readonly ApplicationError OperationFailed =
        new(0x002, nameof(OperationFailed), "Operation Failed", "The operation could not be completed.");

    public static readonly ApplicationError InvalidState =
        new(0x003, nameof(InvalidState), "Invalid State", "The entity is in an invalid state.");

    public static readonly ApplicationError NotAllowed =
        new(0x004, nameof(NotAllowed), "Not Allowed", "The requested action is not permitted.");

    public static readonly ApplicationError LimitExceeded =
        new(0x005, nameof(LimitExceeded), "Limit Exceeded", "An operation exceeded the defined limits.");

    public static readonly ApplicationError Timeout =
        new(0x006, nameof(Timeout), "Timeout", "The operation took too long to complete.");

    public static readonly ApplicationError DependencyUnavailable =
        new(0x007, nameof(DependencyUnavailable), "Dependency Unavailable", "An external dependency is not available.");

    // 0x100 – 0x1FF : Validation / Input
    public static readonly ApplicationError Validation =
        new(0x101, nameof(Validation), "Validation Error", "One or more validation errors occurred.");

    public static readonly ApplicationError Required =
        new(0x102, nameof(Required), "Validation Error", "A required field is missing.");

    public static readonly ApplicationError InvalidFormat =
        new(0x103, nameof(InvalidFormat), "Validation Error", "The format of the provided value is invalid.");

    public static readonly ApplicationError OutOfRange =
        new(0x104, nameof(OutOfRange), "Validation Error", "The provided value is out of range.");

    public static readonly ApplicationError Duplicate =
        new(0x105, nameof(Duplicate), "Conflict", "A duplicate entry was detected.");

    public static readonly ApplicationError NotFound =
        new(0x106, nameof(NotFound), "Not Found", "The requested resource was not found.");

    public static readonly ApplicationError Conflict =
        new(0x107, nameof(Conflict), "Conflict", "The operation conflicts with the current state.");

    // 0x200 – 0x2FF : Security / Permissions
    public static readonly ApplicationError Unauthorized =
        new(0x201, nameof(Unauthorized), "Unauthorized", "Authentication is required to perform this action.");

    public static readonly ApplicationError Forbidden =
        new(0x202, nameof(Forbidden), "Forbidden", "You do not have permission to perform this action.");

    // 0x300 – 0x3FF : Concurrency / State
    public static readonly ApplicationError AlreadyProcessed =
        new(0x301, nameof(AlreadyProcessed), "Conflict", "This action has already been executed.");

    public static readonly ApplicationError Locked =
        new(0x302, nameof(Locked), "Locked", "The resource is currently locked.");

    public static readonly ApplicationError StaleData =
        new(0x303, nameof(StaleData), "Conflict", "The data is outdated due to a concurrency issue.");

    // 0x400 – 0x4FF : External / Integration
    public static readonly ApplicationError UpstreamTimeout =
        new(0x401, nameof(UpstreamTimeout), "Upstream Timeout", "A call to an external service timed out.");

    public static readonly ApplicationError BadGateway =
        new(0x402, nameof(BadGateway), "Bad Gateway", "An upstream service returned an invalid response.");

    public static readonly ApplicationError DependencyFailure =
        new(0x403, nameof(DependencyFailure), "Dependency Failure", "A dependency failed to process the request.");

    public static readonly ApplicationError ExternalServiceUnavailable =
        new(0x404, nameof(ExternalServiceUnavailable), "Service Unavailable", "An external service is currently unavailable.");

    protected CommonErrors()
    {
    }
}