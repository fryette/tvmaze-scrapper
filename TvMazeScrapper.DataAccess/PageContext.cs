using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.Domain;

namespace TvMazeScrapper.DataAccess
{
    public class PageContext : DbContext
    {
        public PageContext(DbContextOptions options) : base(options) { }

        public DbSet<Page> Pages { get; set; }
    }
}
