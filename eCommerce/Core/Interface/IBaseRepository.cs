using eCommerce.Core.entities;
using eCommerce.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog_Project.CORE.@interface
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null, int? pageNumber = 1, int? pageSize = 5, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC");

        public Task<T> Add(T entity);
        public Task<IReadOnlyList<T>> ListAllAsync();

        public Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        public Task<T> Update(T entity);

        public void Delete(T entity);

        public Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);

        public Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null);

        public Task<T> GetEntityWithSpec(ISpecification<T> spec);

        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        public Task<int> CountAsync(ISpecification<T> spec);
        
    }
}
