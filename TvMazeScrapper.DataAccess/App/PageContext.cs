using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.Domain;
using TvMazeScrapper.Domain.App;
using TvMazeScrapper.Domain.TvMaze;

namespace TvMazeScrapper.DataAccess
{
    public class PageContext : DbContext
    {
        public PageContext(DbContextOptions options) : base(options) { }

        public DbSet<Page> Pages { get; set; }

        public DbSet<TvMazePage> TvMazePages { get; set; }

    }
}
