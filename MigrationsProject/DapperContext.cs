using Npgsql;
using System.Data;

namespace MigrationsProject
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection"));
    }
}
