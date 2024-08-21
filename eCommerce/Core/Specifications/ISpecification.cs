using System.Linq.Expressions;

namespace eCommerce.Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesc { get; }
        List<string> includesStrings { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

    }
}
