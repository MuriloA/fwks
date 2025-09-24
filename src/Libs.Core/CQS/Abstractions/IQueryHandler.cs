using System.Threading;
using System.Threading.Tasks;

namespace FwksLabs.Libs.Core.CQS.Abstractions;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}