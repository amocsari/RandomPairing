using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace RandomPairer.Dal.Context
{
    public static class RandomPairerDbContextExtensions
    {
        public static void RegisterSoftDeleteQueryFilters(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes().Where(e => typeof(ISoftDeletableEntity).IsAssignableFrom(e.ClrType));

            foreach (var entityType in entityTypes)
            {
                var parameter = Expression.Parameter(entityType.ClrType);

                var softDeletableProperty = Expression.Property(parameter, nameof(ISoftDeletableEntity.IsDeleted));
                var compareExpression = Expression.MakeBinary(ExpressionType.Equal, softDeletableProperty, Expression.Constant(false));
                var lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
