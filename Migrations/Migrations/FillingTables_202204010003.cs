using FluentMigrator.Runner;
using FluentMigrator;
using Dapper;
using Core.Models;

namespace Migrations.Migrations
{
    [Migration(200204010003)]
    public class FillingTables_202204010003 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("TypeProduct")
                .Row(new { Id = Guid.NewGuid(), Type = "Пицца" })
                .Row(new { Id = Guid.NewGuid(), Type = "Десерты" })
                .Row(new { Id = Guid.NewGuid(), Type = "Закуски" })
                .Row(new { Id = Guid.NewGuid(), Type = "Напитки" });

            Delete.FromTable("PaymentType")
                .Row(new { Id = Guid.NewGuid(), Type = "Безналичный" })
                .Row(new { Id = Guid.NewGuid(), Type = "Наличный" });
        }

        public override void Up()
        {
            Insert.IntoTable("TypeProduct")
                .Row(new { Id = Guid.NewGuid(), Type = "Пицца" })
                .Row(new { Id = Guid.NewGuid(), Type = "Десерты" })
                .Row(new { Id = Guid.NewGuid(), Type = "Закуски" })
                .Row(new { Id = Guid.NewGuid(), Type = "Напитки" });

            Insert.IntoTable("PaymentType")
                .Row(new { Id = Guid.NewGuid(), Type = "Безналичный" })
                .Row(new { Id = Guid.NewGuid(), Type = "Наличный" });
                
        }
    }
}
