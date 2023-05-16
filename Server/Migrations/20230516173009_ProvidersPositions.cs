using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREATIM_naloga.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProvidersPositions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Providers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Providers");
        }
    }
}
