using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202205200009)]
    public class _202205200009_UpdateUser : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Delete.FromTable("User").AllRows();

            Create.Column("UserName").OnTable("User").AsString().Unique().NotNullable();
        }
    }
}
