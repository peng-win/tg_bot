using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202203290002)]
    public class _202203290002_UpdateTables : Migration
    {
        public override void Down()
        {
            Delete.Table("UserStatus");
            Delete.Table("OrderStatus");
            Delete.Table("Unit");
            Delete.Table("Ingridients");
            Delete.Table("TypeProduct");
            Delete.Table("PaymentType");
            Delete.Table("SizeProduct");
            Delete.Table("Products");
            Delete.Table("Menu");
            Delete.Table("User");
            Delete.Table("Order");
        }

        public override void Up()
        {          
            Delete.Table("Order");
            Delete.Table("Menu");
            Delete.Table("User");
            Delete.Table("UserStatus");
            Delete.Table("OrderStatus");
            Delete.Table("Unit");
            Delete.Table("Ingridients");
            Delete.Table("TypeProduct");
            Delete.Table("PaymentType");
            Delete.Table("SizeProduct");
            Delete.Table("Products");

            Create.Table("Ingridients")
                 .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                 .WithColumn("Name").AsString(30).NotNullable().Unique()
                 .WithColumn("WeightInGrams").AsDouble().Nullable()
                 .WithColumn("Price").AsDecimal().NotNullable();

            Create.Table("TypeProduct")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Type").AsString(20).NotNullable().Unique();

            Create.Table("PaymentType")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Type").AsString(20).NotNullable().Unique();

            Create.Table("SizeProduct")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Size").AsDouble().NotNullable()
                .WithColumn("Unit").AsString().NotNullable();

            Create.Table("Products")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Product").AsString(50).NotNullable().Unique()
                .WithColumn("Picture").AsString(100).NotNullable().Unique();

            Create.Table("Menu")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("ProductId").AsGuid().NotNullable().ForeignKey("Products", "Id")
                .WithColumn("Size").AsGuid().Nullable().ForeignKey("SizeProduct", "Id")
                .WithColumn("TypeProduct").AsString(20).NotNullable().ForeignKey("TypeProduct", "Type")
                .WithColumn("WeightInGrams").AsDouble().Nullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("Picture").AsString(100).NotNullable().ForeignKey("Products", "Picture");

            Create.Table("User")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("Patronymic").AsString(100).NotNullable()
                .WithColumn("RegistrationDate").AsDateTime().NotNullable()
                .WithColumn("Phone").AsString(12).NotNullable()
                .WithColumn("Status").AsString(20).NotNullable();

            Create.Table("Order")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("User", "Id")
                .WithColumn("ProductIds").AsString().NotNullable()
                .WithColumn("Address").AsString(1000).NotNullable()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("DateOfCreation").AsDateTime().NotNullable()
                .WithColumn("DateOfCompletion").AsDateTime().Nullable()
                .WithColumn("Description").AsString(1000).Nullable()
                .WithColumn("PaymentType").AsString(20).NotNullable().ForeignKey("PaymentType", "Type")
                .WithColumn("Status").AsString(20).NotNullable();
        }
    }
}
