using FluentMigrator.Runner;
using FluentMigrator;
using Dapper;
using Core.Models;

namespace Migrations.Migrations
{
    [Migration(200204010003)]
    public class _202204010003_FillingTables : Migration
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
            Delete.Column("TypeProduct").FromTable("Menu");
            Delete.Column("Description").FromTable("Menu");
            Delete.Column("Picture").FromTable("Menu");

            Delete.Column("Picture").FromTable("Products");

            Create.Column("TypeProduct").OnTable("Products").AsString(20).NotNullable().ForeignKey("TypeProduct", "Type");
            Create.Column("Description").OnTable("Products").AsString(1000).Nullable();
            Create.Column("Picture").OnTable("Products").AsString(1000).Nullable();

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
