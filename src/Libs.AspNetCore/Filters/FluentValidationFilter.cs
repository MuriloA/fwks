using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Libs.AspNetCore.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Filters;

public sealed class FluentValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestValidator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        var argument = context.Arguments.OfType<T>().FirstOrDefault();

        if (requestValidator is null || argument is null)
            return await next(context);

        var validationResult = await requestValidator.ValidateAsync(argument, context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
            return AppResponses.ValidationErrors(validationResult);

        return await next(context);
    }
}