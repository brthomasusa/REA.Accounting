using REA.Accounting.Infrastructure.Persistence.Interfaces;

namespace REA.Accounting.Infrastructure.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<T> ApplyFilters<T>
        (
            this IQueryable<T> entityCollection,
            IEnumerable<Specification<T>>? filters = null
        )
        {
            if (filters is not null)
                filters.ToList().ForEach(item => entityCollection = entityCollection!.Where(item.ToExpression()));

            return entityCollection;
        }

        public static bool SatisfiesFilters<T>(this T entity, IEnumerable<Specification<T>>? filters = null)
        {
            if (filters is not null)
            {
                foreach (var filter in filters)
                {
                    var IsSatisfied = filter.IsSatisfiedBy(entity);
                    if (!IsSatisfied)
                        return false;
                }
            }

            return true;
        }
    }
}