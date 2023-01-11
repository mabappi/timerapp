using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimerApi.QuartzFacade.Migrations
{
    /// <inheritdoc />
    public partial class Quartz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");
            QuartzMigration.Up(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            QuartzMigration.Down(migrationBuilder);
        }
    }
}
