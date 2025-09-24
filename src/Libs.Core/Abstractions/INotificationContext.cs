using System.Collections.Generic;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Abstractions;

public interface INotificationContext
{
    ApplicationError? Error { get; }
    IDictionary<string, object?> Messages { get; }
    void Add(ApplicationError error, IDictionary<string, object?> messages);
    void Add(ApplicationError error, ValidationResult validationResult);
    void Add(ApplicationError error);
}