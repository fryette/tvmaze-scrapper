using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MazePage.DataModels;
using MazePage.Infrastructure;

namespace MazePage.DataAccess
{
    public class MazePageStore : IMazePageStore
    {
        private string connectionString =
            @"Data Source=EPBYBREW0024\SQLEXPRESS;Initial Catalog=TvMazeScrapper;Integrated Security=True";
        private const string INSERT_PAGE = @"insert MazePage (PageId) values (@PageId)";
        private const string INSERT_SHOWS = @"insert MazeShow (Id, PageId, Name) values (@Id, (SELECT PageId from MazePage WHERE PageId = @PageId), @Name)";
        private const string READ_ITEMS_SQL = @"select * from MazePage, MazeShow
where MazePage.PageId = @PageId and MazeShow.PageId=@PageId";

        public async Task SaveMazePageAsync(MazePageData page)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(INSERT_PAGE, new { PageId = page.Id }, transaction);

                    await connection.ExecuteAsync(
                        INSERT_SHOWS,
                        page.Shows.Select(x => new { x.Id, PageId = page.Id, x.Name }),
                        transaction);

                    transaction.Commit();
                }

                connection.Close();
            }
        }

        public async Task<MazePageData> GetMazePageAsync(int pageId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var items = (await connection.QueryAsync<ShowData>(READ_ITEMS_SQL, new { PageId = pageId })).ToList();

                connection.Close();

                return items.Any() ? new MazePageData { Id = pageId, Shows = items } : null;
            }
        }
    }
}
