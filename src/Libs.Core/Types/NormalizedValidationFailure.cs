using FluentValidation.Results;

namespace FwksLabs.Libs.Core.Types;

public record NormalizedValidationFailure(string PropertyName, string ErrorMessage, int Index = 0)
{
    public static implicit operator NormalizedValidationFailure(ValidationFailure failure) => new(failure.PropertyName, failure.ErrorMessage);
}