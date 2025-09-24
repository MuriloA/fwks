using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.AspNetCore.Extensions;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Filters;

public sealed class FluentValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestValidator = context.GetService<IValidator<T>>();
        var argument = context.Arguments.OfType<T>().FirstOrDefault();

        if (requestValidator is null || argument is null)
            return await next(context);

        var validationResult = await requestValidator.ValidateAsync(argument, context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
            return Responses.Problem(CommonErrors.Validation, context.GetErrorCodeConfiguration().Codes, validationResult.NormalizeErrors());

        return await next(context);
    }
}