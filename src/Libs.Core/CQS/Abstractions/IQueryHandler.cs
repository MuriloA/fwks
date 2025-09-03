using System.Threading;
using System.Threading.Tasks;

namespace FwksLabs.Libs.Core.CQS.Abstractions;

public interface IQueryHandler<in TQuery>
    where TQuery : IQuery
{
    ValueTask HandleAsync(TQuery operation, CancellationToken cancellationToken);
}

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
    where TResult : notnull
{
    ValueTask<TResult> HandleAsync(TQuery operation, CancellationToken cancellationToken);
}