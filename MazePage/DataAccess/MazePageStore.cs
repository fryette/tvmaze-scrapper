using System.Collections.Generic;
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
        private const string CONNECTION_STRING =
            @"Data Source=EPBYBREW0024\SQLEXPRESS;Initial Catalog=TvMazeScrapper;Integrated Security=True";
        private const string INSERT_SHOWS = @"insert MazeShow (Id, PageId, Name) values (@Id, @PageId, @Name)";
        private const string READ_ITEMS_SQL = @"select * from MazeShow WHERE MazeShow.PageId=@PageId";

        public async Task SaveMazePageAsync(int pageId, IEnumerable<Show> shows)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(
                        INSERT_SHOWS,
                        shows.Select(x => new { x.Id, PageId = pageId, x.Name }),
                        transaction);

                    transaction.Commit();
                }

                connection.Close();
            }
        }

        public async Task<List<Show>> LoadShowsByPageIdAsync(int pageId)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                var items = (await connection.QueryAsync<Show>(READ_ITEMS_SQL, new { PageId = pageId })).ToList();

                connection.Close();

                return items;
            }
        }
    }
}
