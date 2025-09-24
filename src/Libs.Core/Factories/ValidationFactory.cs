using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Types;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Core.Factories;

public sealed class ValidationFactory(
    IServiceProvider serviceProvider,
    INotificationContext notificationContext) : IValidationFactory
{
    public async Task<bool> ValidateAsync<T>(T instance, ApplicationError error, CancellationToken cancellationToken)
    {
        var validationResult = await Get<T>().ValidateAsync(instance, cancellationToken);

        if (validationResult.IsValid)
            return true;

        notificationContext.Add(error, validationResult);

        return false;
    }

    public IValidator<T> Get<T>() =>
        serviceProvider.GetRequiredService<IValidator<T>>();
}