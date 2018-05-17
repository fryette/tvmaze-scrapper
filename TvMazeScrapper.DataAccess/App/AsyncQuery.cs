using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TvMazeScrapper.DataAccess.App
{
    public class AsyncQuery<T> where T : class
    {
        private readonly IQueryable<T> _query;

        public AsyncQuery(IQueryable<T> dbSet)
        {
            _query = dbSet;
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return _query.FirstOrDefaultAsync(expression);
        }
    }
}