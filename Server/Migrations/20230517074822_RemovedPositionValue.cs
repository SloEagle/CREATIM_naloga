using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREATIM_naloga.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPositionValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Providers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
