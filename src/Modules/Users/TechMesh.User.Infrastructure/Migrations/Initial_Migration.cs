using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMesh.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "technologies",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR", maxLength: 256, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR", maxLength: 11, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    Neighborhood = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    City = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR", maxLength: 128, nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    Complement = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTechnology",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnologyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTechnology", x => new { x.UserId, x.TechnologyId });
                    table.ForeignKey(
                        name: "FK_UserTechnology_technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalSchema: "user",
                        principalTable: "technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTechnology_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTechnology_TechnologyId",
                schema: "user",
                table: "UserTechnology",
                column: "TechnologyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTechnology",
                schema: "user");

            migrationBuilder.DropTable(
                name: "technologies",
                schema: "user");

            migrationBuilder.DropTable(
                name: "users",
                schema: "user");
        }
    }
}
