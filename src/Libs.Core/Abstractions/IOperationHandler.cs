using System.Threading;
using System.Threading.Tasks;

namespace FwksLabs.Libs.Core.Abstractions;

public interface IOperationHandler<in TOperation>
    where TOperation : IOperation
{
    ValueTask HandleAsync(TOperation operation, CancellationToken cancellationToken = default);
}

public interface IOperationHandler<in TOperation, TResult>
    where TOperation : IOperation<TResult>
    where TResult : notnull
{
    ValueTask<TResult> HandleAsync(TOperation operation, CancellationToken cancellationToken = default);
}