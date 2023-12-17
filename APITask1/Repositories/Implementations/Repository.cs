


using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;

namespace APITask1.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T:BaseEntity,new()
    {
        private readonly DbSet<T> _table;
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
           _table = context.Set<T>();
           _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T,
            bool>>? expression = null, 
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescenting=false,
            int skip=0,
            int take=0,
            bool isTracking=true,
            params string[] include)
        {
            var query = _table.AsQueryable();

            if (expression is not null)   query = query.Where(expression);
        
            if (orderExpression is not null)
            {
                if (isDescenting)  query = query.OrderByDescending(orderExpression);
               
                else query = query.OrderBy(orderExpression);   
            }
            if (skip!=0)  query = query.Skip(skip);
            
            if (take!=0)  query = query.Take(take);
            
            if (include is not null)
            {
                for (int i = 0; i < include.Length; i++)
                {
                    query = query.Include(include[i]); 
                }
            }
            return isTracking?query:query.AsNoTracking();

             
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}
