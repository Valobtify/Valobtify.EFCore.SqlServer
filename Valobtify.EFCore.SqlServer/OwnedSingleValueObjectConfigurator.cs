using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valobtify.EFCore.Core.Abstraction;

namespace Valobtify.EFCore.SqlServer;

internal class OwnedSingleValueObjectConfigurator : IOwnedSingleValueObjectConfigurator
{
    public static EntityTypeBuilder<TEntity> ConfigureOwnedSingleValueObject<TEntity, TRelatedEntity,
        TSingleValueObjectType>(
        EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TRelatedEntity?>> navigationExpression, int? maxLength = null)
        where TEntity : class
        where TRelatedEntity : SingleValueObject<TRelatedEntity, TSingleValueObjectType>,
        ICreatableValueObject<TRelatedEntity, TSingleValueObjectType>
        where TSingleValueObjectType : notnull
    {
        builder.OwnsOne(navigationExpression, action =>
        {
            string columnName;

            if (navigationExpression.Body is MemberExpression expression)
                columnName = expression.Member.Name;

            else throw new ArgumentException("Expression must be a member expression", nameof(navigationExpression));

            action.Property(entity => entity.Value).HasColumnName(columnName);

            action.Property(entity => entity.Value).HasMaxLength(maxLength ?? -1);
        });

        return builder;
    }
}