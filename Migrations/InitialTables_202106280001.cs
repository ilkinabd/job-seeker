namespace JobSeekerApi.Migrations;
using FluentMigrator;

[Migration(202106280001)]
public class InitialTables_202106280001 : Migration
{
    public override void Down()
    {
        Delete.Table("users");
    }
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("user_type_id").AsInt16().NotNullable()
            .WithColumn("email").AsString(255).NotNullable()
            .WithColumn("password").AsCustom("text").NotNullable()
            .WithColumn("date_of_birth").AsDateTime().NotNullable()
            .WithColumn("gender").AsString(1).NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable()
            .WithColumn("contact_number").AsString(255).NotNullable()
            .WithColumn("sms_notification_active").AsBoolean().NotNullable()
            .WithColumn("email_notification_active").AsBoolean().NotNullable()
            .WithColumn("image").AsBinary().NotNullable()
            .WithColumn("created_at").AsDateTime().WithDefaultValue("NOW()")
            .WithColumn("updated_at").AsDateTime().WithDefaultValue("NOW()")
            .WithColumn("deleted_at").AsDateTime().Nullable();
    }
}