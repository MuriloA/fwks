using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Libs.Infra.Postgres.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.Underscore());
            
            entity.SetSchema(entity.GetSchema()?.Underscore());

            // Columns
            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().Underscore());

            // Keys
            foreach (var key in entity.GetKeys())
                key.SetName(key.GetName()?.Underscore());

            // Foreign keys
            foreach (var fk in entity.GetForeignKeys())
                fk.SetConstraintName(fk.GetConstraintName()?.Underscore());

            // Indexes
            foreach (var index in entity.GetIndexes())
                index.SetDatabaseName(index.GetDatabaseName()?.Underscore());
        }

        return modelBuilder;
    }
}