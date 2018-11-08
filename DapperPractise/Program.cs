using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;

namespace DapperPractise
{
    class Program
    {
        static private string deleteSql = "DELETE FROM Track WHERE TrackId = @ID";
        static private string insertSql = "INSERT INTO Track (TrackId, Name) Values (@Id, @NameToFind);";
        static private string updateSql = "UPDATE Track SET Name = @Name WHERE TrackId = @Id;";

        static void Main(string[] args)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TrackDB"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                var insertRows = db.Execute(insertSql, new[]
                    {
                        new {Id = 6, NameToFind = "John" },
                        new {Id = 7, NameToFind = "Andy" },
                        new {Id = 8, NameToFind = "Allan" }
                    });
                var updateRows = db.Execute(updateSql, new { Id = 1, Name = "Mark" });
                var deleteRow = db.Execute(deleteSql, new { Id = 8 });
                IEnumerable<Track> tracks = db.Query<Track>("SELECT * FROM Track WHERE TrackId = @Id",
                    new 
                    {
                        Id = 1
                    });
                Console.ReadKey();
            }
        }
    }
}
