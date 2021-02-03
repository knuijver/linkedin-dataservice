using Microsoft.EntityFrameworkCore.Migrations;

namespace SAWebHost.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokenEntry_LinkedInProvider_ProviderId",
                schema: "fan",
                table: "AccessTokenEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokenEntry_Organization_OrganizationId",
                schema: "fan",
                table: "AccessTokenEntry");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokenEntry_LinkedInProvider_ProviderId",
                schema: "fan",
                table: "AccessTokenEntry",
                column: "ProviderId",
                principalSchema: "fan",
                principalTable: "LinkedInProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokenEntry_Organization_OrganizationId",
                schema: "fan",
                table: "AccessTokenEntry",
                column: "OrganizationId",
                principalSchema: "fan",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokenEntry_LinkedInProvider_ProviderId",
                schema: "fan",
                table: "AccessTokenEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokenEntry_Organization_OrganizationId",
                schema: "fan",
                table: "AccessTokenEntry");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokenEntry_LinkedInProvider_ProviderId",
                schema: "fan",
                table: "AccessTokenEntry",
                column: "ProviderId",
                principalSchema: "fan",
                principalTable: "LinkedInProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokenEntry_Organization_OrganizationId",
                schema: "fan",
                table: "AccessTokenEntry",
                column: "OrganizationId",
                principalSchema: "fan",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
