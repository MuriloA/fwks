using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Configuration;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    private static readonly string EntityName = typeof(TEntity).Name.PluralizeEntity();

    protected virtual string TableName { get; } = EntityName;
    protected virtual string SchemaName { get; } = "App";
    protected virtual string PrimaryKeyName { get; set; } = $"PK_{EntityName}";

    protected virtual void Extend(EntityTypeBuilder<TEntity> builder) { }

    protected virtual void ConfigureIds(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(x => x.Id)
            .HasName(PrimaryKeyName);
    }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName, SchemaName);

        ConfigureIds(builder);

        Extend(builder);
    }
}