using Microsoft.EntityFrameworkCore.Migrations;

namespace SAWebHost.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fan");

            migrationBuilder.CreateTable(
                name: "LinkedInProvider",
                schema: "fan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedInProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "fan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessTokenEntry",
                schema: "fan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrganizationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiresIn = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiresIn = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<string>(type: "nvarchar(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTokenEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessTokenEntry_LinkedInProvider_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "fan",
                        principalTable: "LinkedInProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccessTokenEntry_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "fan",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokenEntry_OrganizationId",
                schema: "fan",
                table: "AccessTokenEntry",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokenEntry_ProviderId",
                schema: "fan",
                table: "AccessTokenEntry",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedInProvider_ApplicationName",
                schema: "fan",
                table: "LinkedInProvider",
                column: "ApplicationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LinkedInProvider_ClientId",
                schema: "fan",
                table: "LinkedInProvider",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_Name",
                schema: "fan",
                table: "Organization",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessTokenEntry",
                schema: "fan");

            migrationBuilder.DropTable(
                name: "LinkedInProvider",
                schema: "fan");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "fan");
        }
    }
}
