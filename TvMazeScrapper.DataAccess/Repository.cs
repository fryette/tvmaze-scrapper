using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.DataAccess.App;

namespace TvMazeScrapper.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShowsContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly AsyncQuery<T> _query;

        public Repository(ShowsContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
            _query = new AsyncQuery<T>(_dbSet.AsQueryable());
        }

        public async Task SaveAsync(T model)
        {
            await _dbSet.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public AsyncQuery<T> QueryAsync()
        {
            return _query;
        }
    }
}