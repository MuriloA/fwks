using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Abstractions;

public interface IValidationFactory
{
    Task<bool> ValidateAsync<T>(T instance, ApplicationError error, CancellationToken cancellationToken);
}