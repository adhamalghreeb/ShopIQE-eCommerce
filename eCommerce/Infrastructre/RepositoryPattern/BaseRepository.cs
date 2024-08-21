using Blog_Project.CORE.@interface;
using eCommerce.Core.Data;
using eCommerce.Core.entities;
using eCommerce.Core.Specifications;
using eCommerce.Infrastructre.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog_Project.EF.RepositoryPattern
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly appDBcontext appDBcontext;

        public BaseRepository(appDBcontext appDBcontext)
        {
            this.appDBcontext = appDBcontext;
        }

        public async Task<T> Add(T entity)
        {
            await appDBcontext.Set<T>().AddAsync(entity);
            return entity;

        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await appDBcontext.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await appDBcontext.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T entity)
        {
            appDBcontext.Update(entity);
            return entity;
        }

        public async void Delete(T entity)
        {
            appDBcontext.Set<T>().Remove(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await appDBcontext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null, int? pageNumber = 1, int? pageSize = 5, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = appDBcontext.Set<T>().Where(criteria);

            // includes all
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);


            // sort
            if (orderBy != null)
            {
                if (orderByDirection == orderByDirection)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            // paginagtion
            var skipResult = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResult ?? 0).Take(pageSize ?? 5);

            return await query.ToListAsync();
        }

        

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = appDBcontext.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = appDBcontext.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(appDBcontext.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}