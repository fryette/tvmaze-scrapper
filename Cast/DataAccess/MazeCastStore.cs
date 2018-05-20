
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Cast.DataAccess
{
    public class MazeCastStore : IMazeCastStore
    {
        private string connectionString =
            @"Data Source=EPBYBREW0024\SQLEXPRESS;Initial Catalog=TvMazeScrapper;Integrated Security=True";
        private const string INSERT_SSHOWS_IDS = @"insert DownloadedShows (Id) values (@ShowId)";
        private const string INSERT_PERSONS = @"insert MazePerson (Id, MazeShowId, Birthday,Name)
values (@Id, (SELECT MazeShowId from DownloadedShows WHERE Id = @ShowId), @Birthday,@Name)";
        private const string READ_ITEMS_SQL = @"select * from MazePage, MazeShow
where MazePage.PageId = @PageId and MazeShow.PageId=@PageId";

        public async Task SaveCastsAsync(List<DataModels.Cast> casts)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(INSERT_SSHOWS_IDS, casts.Select(x => new { ShowId = x.ShowId }), transaction);

                    var objects = casts.Select(
                        x => x.Persons.Select(
                            p => new
                            {
                                ShowId = x.ShowId,
                                Id = p.Id,
                                Birthday = p.Birthday,
                                Name = p.Name
                            })).ToList();

                    await connection.ExecuteAsync(INSERT_PERSONS, objects, transaction);

                    transaction.Commit();
                }

                connection.Close();
            }
        }

        //public async Task<MazePageData> GetMazePageAsync(int pageId)
        //{
        //    //using (var connection = new SqlConnection(connectionString))
        //    //{
        //    //    await connection.OpenAsync();

        //    //    var items = (await connection.QueryAsync<ShowData>(READ_ITEMS_SQL, new { PageId = pageId })).ToList();

        //    //    connection.Close();

        //    //    return items.Any() ? new MazePageData { Id = pageId, Shows = items } : null;
        //    //}
        //}
    }
}
