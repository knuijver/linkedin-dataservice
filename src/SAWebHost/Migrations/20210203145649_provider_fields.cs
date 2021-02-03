using Microsoft.EntityFrameworkCore.Migrations;

namespace SAWebHost.Migrations
{
    public partial class provider_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizationEndpoint",
                schema: "fan",
                table: "LinkedInProvider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                schema: "fan",
                table: "LinkedInProvider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TokenEndpoint",
                schema: "fan",
                table: "LinkedInProvider",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationEndpoint",
                schema: "fan",
                table: "LinkedInProvider");

            migrationBuilder.DropColumn(
                name: "Scope",
                schema: "fan",
                table: "LinkedInProvider");

            migrationBuilder.DropColumn(
                name: "TokenEndpoint",
                schema: "fan",
                table: "LinkedInProvider");
        }
    }
}
