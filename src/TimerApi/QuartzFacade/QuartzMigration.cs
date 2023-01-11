using Microsoft.EntityFrameworkCore.Migrations;
namespace TimerApi.QuartzFacade;
public class QuartzMigration
{
    private const string UpScriptPath = @"QuartzFacade/SqlScripts/tables_mysql_innodb_up.sql";
    private const string DownScriptPath = @"QuartzFacade/SqlScripts/tables_mysql_innodb_down.sql";
    public static void Up(MigrationBuilder migrationBuilder) => migrationBuilder.Sql(File.ReadAllText(UpScriptPath));
    public static void Down(MigrationBuilder migrationBuilder) => migrationBuilder.Sql(File.ReadAllText(DownScriptPath));
}

