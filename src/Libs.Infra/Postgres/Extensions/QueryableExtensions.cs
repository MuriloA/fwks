using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Types;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Libs.Infra.Postgres.Extensions;

public static class QueryableExtensions
{
    public static Task<(int TotalItems, List<TEntity> Items)> GetPageAsync<TEntity>(
        this IQueryable<TEntity> queryable, PageQuery pageQuery, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return queryable.GetPageAsync<TEntity, Guid>(pageQuery, _ => true, false, cancellationToken);
    }

    public static Task<(int TotalItems, List<TEntity> Items)> GetPageAsync<TEntity, TKey>(
        this IQueryable<TEntity> queryable, PageQuery pageQuery, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        return queryable.GetPageAsync<TEntity, TKey>(pageQuery, _ => true, false, cancellationToken);
    }

    public static Task<(int TotalItems, List<TEntity> Items)> GetAndTrackPageAsync<TEntity>(
        this IQueryable<TEntity> queryable, PageQuery pageQuery, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return queryable.GetPageAsync<TEntity, Guid>(pageQuery, _ => true, true, cancellationToken);
    }

    public static Task<(int TotalItems, List<TEntity> Items)> GetAndTrackPageAsync<TEntity, TKey>(
        this IQueryable<TEntity> queryable, PageQuery pageQuery, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        return queryable.GetPageAsync<TEntity, TKey>(pageQuery, _ => true, true, cancellationToken);
    }

    public static async Task<(int TotalItems, List<TEntity> Items)> GetPageAsync<TEntity, TKey>(
        this IQueryable<TEntity> dbSet, PageQuery pageQuery, Expression<Func<TEntity, bool>> predicate, bool track = false, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        var query = track ? dbSet.Where(predicate) : dbSet.AsNoTracking().Where(predicate);

        query = pageQuery.PageSize < 1 ? query : query.Skip(pageQuery.GetSkip()).Take(pageQuery.PageSize);

        return (
            await dbSet.CountAsync(predicate, cancellationToken),
            await query.ToListAsync(cancellationToken));
    }
}