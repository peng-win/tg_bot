using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;

namespace Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<string> GetPizza()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" " +
                    $"FROM \"Products\" " +
                    $"WHERE \"TypeProduct\" = 'Пицца' ");
            }
        }        
       
        public IEnumerable<string> GetDesserts()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" " +
                    $"FROM \"Products\" " +
                    $"WHERE \"TypeProduct\" = 'Десерты'");
            }
        }

        public IEnumerable<string> GetDrinks()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\" = 'Напитки'");
            }
        }

        public IEnumerable<string> GetSnacks()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\" = 'Закуски'");
            }
        }
    }
}
