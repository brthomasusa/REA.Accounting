using System.Linq.Expressions;

namespace REA.Accounting.Infrastructure.Persistence.Specifications
{
    public abstract class Specification<TEntity>
    {
        protected Specification(Expression<Func<TEntity, bool>> criteria)
            => Criteria = criteria;

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();

        public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
            => IncludeExpressions.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => OrderByExpression = orderByExpression;

        protected void OrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
            => OrderByDescendingExpression = orderByDescendingExpression;
    }
}