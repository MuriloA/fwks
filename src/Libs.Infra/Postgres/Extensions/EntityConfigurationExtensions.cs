using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Extensions;

public static class EntityConfigurationExtensions
{
    public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> WithKeyedConstraintName<TEntity, TRelatedEntity>(
        this ReferenceCollectionBuilder<TEntity, TRelatedEntity> referenceCollectionBuilder, params string[] keys)
        where TEntity : class where TRelatedEntity : class
    {
        return referenceCollectionBuilder.HasConstraintName($"FK_{string.Join('_', keys)}");
    }

    public static OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOneAsJson<TEntity, TRelatedEntity>(
        this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, TRelatedEntity>> propertyExpression, bool formatColumnName = true)
        where TEntity : class
        where TRelatedEntity : class
    {
        var propertyName = GetPropertyName(propertyExpression);

        if (formatColumnName)
            propertyName = propertyName.Underscore();

        return builder.OwnsOne(propertyExpression!).ToJson(propertyName);
    }

    public static OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsManyAsJson<TEntity, TRelatedEntity>(
        this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> propertyExpression, bool formatColumnName = true)
        where TEntity : class
        where TRelatedEntity : class
    {
        var propertyName = GetPropertyName(propertyExpression);

        if (formatColumnName)
            propertyName.Underscore();

        return builder.OwnsMany(propertyExpression!).ToJson(propertyName);
    }

    private static string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member)
            return member.Member.Name;

        throw new ArgumentException("Expression must be a property access", nameof(expression));
    }
}