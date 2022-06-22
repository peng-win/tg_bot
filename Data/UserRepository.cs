using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<string> GetPhone()
        {
            using (IDbConnection db = new NpgsqlConnection
                (_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Phone\" FROM \"User\"");
            }
        }

        public IEnumerable<string> GetNickName()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"UserName\" FROM \"User\"");
            }
        }
       
    }
}
