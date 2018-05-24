using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cast.DataModels;
using Cast.Infrastructure;
using Dapper;

namespace Cast.DataAccess
{
    public class MazeCastStore : IMazeCastStore
    {
        private const string CONNECTION_STRING = "Your Connection String";

        private const string INSERT_SHOWS_IDS = @"MERGE INTO DownloadedShows AS Target
USING (SELECT @Id as Id) as Source
ON (Target.Id=Source.Id)
WHEN MATCHED THEN
UPDATE SET Target.Id=@Id
WHEN NOT MATCHED THEN 
INSERT (Id) VALUES (@Id);";
        private const string INSERT_PERSONS = @"MERGE INTO MazePerson WITH (HOLDLOCK) AS Target
USING (SELECT @Id as Id) as Source
ON (Target.Id=Source.Id)
WHEN MATCHED THEN
UPDATE SET Target.Id=@Id, Target.Birthday=@Birthday, Target.Name=@Name
WHEN NOT MATCHED THEN 
INSERT (Id, Birthday,Name) VALUES (@Id, @Birthday,@Name);";
        private const string INSERT_SHOWSPERSON = @"INSERT ShowsPersons (ShowId, PersonId) 
VALUES ((SELECT Id from DownloadedShows WHERE Id = @ShowId), (SELECT Id from MazePerson WHERE Id = @PersonId))";
        private const string READ_ITEMS_SQL = @"SELECT ShowsPersons.ShowId, MazePerson.Id, MazePerson.Birthday, MazePerson.Name
FROM ShowsPersons 
INNER JOIN MazePerson 
ON ShowsPersons.ShowId IN @ids AND ShowsPersons.PersonId=MazePerson.Id";

        public async Task SaveCastsAsync(List<DataModels.Cast> casts)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(INSERT_SHOWS_IDS, casts.Select(x => new { Id = x.ShowId }), transaction);
                    await connection.ExecuteAsync(INSERT_PERSONS, casts.SelectMany(x => x.Persons), transaction);
                    await connection.ExecuteAsync(
                        INSERT_SHOWSPERSON,
                        casts.Select(x => x.Persons.Select(y => new { x.ShowId, PersonId = y.Id })).SelectMany(x => x),
                        transaction);

                    transaction.Commit();
                }

                connection.Close();
            }
        }

        public async Task<List<DataModels.Cast>> GetCastsAsync(IReadOnlyList<int> ids)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                await connection.OpenAsync();

                var result = new Dictionary<int, DataModels.Cast>();

                await connection.QueryAsync<int, Person, int>(
                    READ_ITEMS_SQL,
                    (id, person) =>
                    {
                        if (!result.ContainsKey(id))
                        {
                            result.Add(id, new DataModels.Cast { ShowId = id, Persons = new List<Person>() });
                        }

                        if (person != null)
                        {
                            result[id].Persons.Add(person);
                        }

                        return id;
                    },
                    new { ids });

                connection.Close();

                return result.Values.ToList();
            }
        }
    }
}