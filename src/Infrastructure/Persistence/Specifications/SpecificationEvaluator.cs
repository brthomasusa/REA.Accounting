using Microsoft.EntityFrameworkCore;

namespace REA.Accounting.Infrastructure.Persistence.Specifications
{
    public static class SpecificationEvaluator
    {
        //TODO How to add AsNoTracking to this.
        public static IQueryable<TEntity> GetQuery<TEntity>
        (
            IQueryable<TEntity> inputQueryable,
            Specification<TEntity> specification
        ) where TEntity : class
        {
            IQueryable<TEntity> queryable = inputQueryable;

            if (specification.Criteria is not null)
                queryable = queryable.Where(specification.Criteria);

            specification.IncludeExpressions.Aggregate
            (
                queryable,
                (current, includeExpression) => current.Include(includeExpression)
            );

            if (specification.OrderByExpression is not null)
            {
                queryable = queryable.OrderBy(specification.OrderByExpression);
            }
            else if (specification.OrderByDescendingExpression is not null)
            {
                queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
            }

            return queryable;
        }
    }
}