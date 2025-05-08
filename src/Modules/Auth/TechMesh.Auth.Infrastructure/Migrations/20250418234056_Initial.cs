using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMesh.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    Status = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR", maxLength: 256, nullable: false),
                    Status = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Type = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                schema: "auth",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "auth",
                table: "User",
                column: "RoleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "User",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "auth");
        }
    }
}
