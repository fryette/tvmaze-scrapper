using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.Domain;
using TvMazeScrapper.Domain.App;
using TvMazeScrapper.Domain.TvMaze;

namespace TvMazeScrapper.DataAccess
{
    public class ShowsContext : DbContext
    {
        public ShowsContext(DbContextOptions options) : base(options) { }

        public DbSet<Page> Pages { get; set; }

        public DbSet<TvMazePage> TvMazePages { get; set; }

    }
}
