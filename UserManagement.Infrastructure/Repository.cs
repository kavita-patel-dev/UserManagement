using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Interfaces;

namespace UserManagement.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        
        public async Task<(IEnumerable<T> Data, int TotalCount)> GetAllAsync(int skip = 0, int take = 50)
        {
            var totalCount = await _dbSet.CountAsync();
            var query = _dbSet.AsNoTracking();
            var data = await query.Skip(skip).Take(take).ToListAsync();
            return (data, totalCount);
        }
    }
}
