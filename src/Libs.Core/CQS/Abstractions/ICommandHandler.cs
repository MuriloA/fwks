using System.Threading;
using System.Threading.Tasks;

namespace FwksLabs.Libs.Core.CQS.Abstractions;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    ValueTask HandleAsync(TCommand operation, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
    where TResult : notnull
{
    ValueTask<TResult> HandleAsync(TCommand operation, CancellationToken cancellationToken);
}