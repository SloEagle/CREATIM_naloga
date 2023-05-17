using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREATIM_naloga.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedSidTokenUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SID",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Providers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SID",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Providers");
        }
    }
}
