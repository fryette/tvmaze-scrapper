using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.Domain;
using TvMazeScrapper.Models;

namespace TvMazeScrapper.WebApi.DbContexts
{
    public class PageContext : DbContext
    {
        public PageContext(DbContextOptions options) : base(options) { }

        public DbSet<Page> Pages { get; set; }
    }
}
