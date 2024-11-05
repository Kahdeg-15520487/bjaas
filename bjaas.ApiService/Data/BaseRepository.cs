using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace bjaas.ApiService.Data
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly BlackjackDBContext _context;

        public BaseRepository(BlackjackDBContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter);
        }
    }
}
