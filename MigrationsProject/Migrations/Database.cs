using Dapper;
using Npgsql;

namespace MigrationsProject.Migrations
{
    public class Database
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _configuration;
        public Database(DapperContext context, IConfiguration configuration)
        {
            _context = context;            
            _configuration = configuration;
        }
        public void CreateDatabase(string dbName)
        {/*
            dbName = "PizzaDeliveryDb";
            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                string sql = $"SELECT DATNAME FROM pg_catalog.pg_database WHERE DATNAME = '{dbName}'";
                //string sql = $"USING PizzaDeliveryDb";
                using (NpgsqlCommand command = new NpgsqlCommand
                    (sql, conn))
                {
                    try
                    {
                        conn.Open();
                        var i = command.ExecuteScalar();
                        conn.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }*/
            
            var query = $"SELECT * FROM pg_catalog.pg_database WHERE DATNAME = '{dbName}'";
            var parameters = new DynamicParameters();
            parameters.Add("DATNAME", dbName);

            using (var connection = _context.CreateConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }

        }
    }
}
