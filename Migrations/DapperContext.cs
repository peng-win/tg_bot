﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations
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
