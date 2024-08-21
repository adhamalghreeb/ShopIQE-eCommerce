using System.Linq.Expressions;

namespace eCommerce.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T>
    {
        public BaseSpecifications()
        {
            
        }
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria {  get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled => throw new NotImplementedException();



        public List<string> includesStrings { get; } = [];

        protected void AddInclude(Expression<Func<T, object>> include) { 
            Includes.Add(include); 
        }

        protected void AddIncludeString(string includeString)
        {
            includesStrings.Add(includeString);
        }
        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void ApplyPaging(int skip , int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
