using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
    /*
    public int InsertWithSql()
    {
        using (var conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=4815162342;Database=tgbotdb")) 
        {
            User user = new User();
            user.FirstName = "Dapper01";
            user.LastName = "Zhoukou";
            user.Patronymic = "15";
            user.Phone = "15";
            // string _sql = "INSERT INTO User (имя, адрес, возраст) VALUES ('Dapper01', ' ', 13)";
            string _sql = "INSERT INTO User(name,address,age)VALUES(@name,@address,@age)";
            return conn.Execute(_sql, user);
        }
    }*/
}
