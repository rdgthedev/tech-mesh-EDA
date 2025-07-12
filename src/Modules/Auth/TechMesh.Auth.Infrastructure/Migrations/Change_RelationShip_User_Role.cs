using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMesh.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_RelationShip_User_Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                schema: "auth",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "auth",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                schema: "auth",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "auth",
                table: "User",
                column: "RoleId",
                unique: true);
        }
    }
}
