using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202203220001)]
    public class InitialTables_202203220001 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("UserStatus")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Status").AsString(20).NotNullable();

            Create.Table("OrderStatus")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Status").AsString(20).NotNullable();

            Create.Table("Unit")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(10).NotNullable();

            Create.Table("Ingridients")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(30).NotNullable()
                .WithColumn("WeightInGrams").AsDouble().Nullable()
                .WithColumn("Price").AsDecimal().NotNullable();

            Create.Table("TypeProduct")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Type").AsString(20).NotNullable();

            Create.Table("PaymentType")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Type").AsString(20).NotNullable();

            Create.Table("SizeProduct")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Size").AsString(20).NotNullable()
                .WithColumn("Diameter").AsDouble().NotNullable()
                .WithColumn("Unit").AsString(10).NotNullable();

            Create.Table("Menu")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Product").AsString(50).NotNullable()
                .WithColumn("Size").AsString(20).Nullable().ForeignKey("SizeProduct", "Size")
                .WithColumn("TypeProduct").AsString(20).NotNullable().ForeignKey("TypeProduct", "Type")
                .WithColumn("WeightInGrams").AsDouble().Nullable()
                .WithColumn("Price").AsDecimal().NotNullable();

            Create.Table("User")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FIO").AsString(100).NotNullable()
                .WithColumn("RegistrationDate").AsDateTime().NotNullable()
                .WithColumn("Phone").AsString(12).NotNullable()
                .WithColumn("Status").AsString(20).NotNullable().ForeignKey("UserStatus", "Status");

            Create.Table("Order")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("User", "Id")
                .WithColumn("Product1").AsString(50).NotNullable().ForeignKey("Menu", "Product")
                .WithColumn("Product2").AsString(50).Nullable().ForeignKey("Menu", "Product")
                .WithColumn("Product3").AsString(50).Nullable().ForeignKey("Menu", "Product")
                .WithColumn("Product4").AsString(50).Nullable().ForeignKey("Menu", "Product")
                .WithColumn("Product5").AsString(50).Nullable().ForeignKey("Menu", "Product")
                .WithColumn("Address").AsString(1000).NotNullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("DateOfCreation").AsDateTime().NotNullable()
                .WithColumn("DateOfCompletion").AsDateTime().Nullable()
                .WithColumn("Description").AsString(1000).Nullable()
                .WithColumn("PaymentType").AsString(20).NotNullable().ForeignKey("PaymentType", "Type")
                .WithColumn("Status").AsString(20).NotNullable().ForeignKey("OrderStatus", "Satus");
        }
    }
}
