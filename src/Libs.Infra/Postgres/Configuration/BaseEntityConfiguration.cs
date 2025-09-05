using System;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Configuration;

public class BaseEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity, Guid>
    where TEntity : class, IEntity<Guid>;

public class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    private static readonly string EntityName = typeof(TEntity).Name.PluralizeEntity();

    protected virtual string TableName { get; } = EntityName;
    protected virtual string SchemaName { get; } = "App";
    protected virtual string PrimaryKeyName { get; set; } = $"PK_{EntityName}";

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName, SchemaName);

        ConfigureIds(builder);

        Extend(builder);
    }

    protected virtual void Extend(EntityTypeBuilder<TEntity> builder)
    {
        // Customize extensions
    }

    protected virtual void ConfigureIds(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .HasName(PrimaryKeyName);
    }
}