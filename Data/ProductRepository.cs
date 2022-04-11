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
        public IEnumerable<string> GetAllProducts()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Products\".\"Product\" FROM \"Products\" WHERE \"Products\".\"Product\" = \"Menu\".\"Product\"");                
            }
        }

        public IEnumerable<string> GetPizza()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Products\".\"Product\" FROM \"Menu\", \"Products\" WHERE \"Products\".\"TypeProduct\" = 'Пицца' AND \"Products\".\"Product\" = \"Menu\".\"Product\"");
            }
        }

        public IEnumerable<string> GetDesserts()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\" = 'Десерты'");
            }
        }

        public IEnumerable<string> GetDrinks()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\" = 'Напитки'");
            }
        }

        public IEnumerable<string> GetSnacks()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Product\" FROM \"Products\" WHERE \"TypeProduct\" = 'Закуски'");
            }
        }

        public IEnumerable<string> GetSizePizza()
        {
            using (IDbConnection db = new NpgsqlConnection(_configuration.GetConnectionString("PostreSQLConnection")))
            {
                return db.Query<string>($"SELECT \"Menu\".\"Price\", \"Unit\" FROM \"Menu\", \"Products\", \"SizeProduct\" " +
                    $"WHERE \"Products\".\"TypeProduct\" = 'Пицца' " +
                    $"AND \"Menu\".\"Product\" = \"Products\".\"Product\"" +
                    $"AND \"Menu\".\"Size\" = \"SizeProduct\".\"Size\"");
            }
        }
    }
}
