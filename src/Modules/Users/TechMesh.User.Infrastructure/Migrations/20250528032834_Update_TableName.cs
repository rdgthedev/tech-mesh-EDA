using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMesh.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_TableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTechnology_technologies_TechnologyId",
                schema: "user",
                table: "UserTechnology");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTechnology_users_UserId",
                schema: "user",
                table: "UserTechnology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTechnology",
                schema: "user",
                table: "UserTechnology");

            migrationBuilder.RenameTable(
                name: "UserTechnology",
                schema: "user",
                newName: "users_technologies",
                newSchema: "user");

            migrationBuilder.RenameIndex(
                name: "IX_UserTechnology_TechnologyId",
                schema: "user",
                table: "users_technologies",
                newName: "IX_users_technologies_TechnologyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users_technologies",
                schema: "user",
                table: "users_technologies",
                columns: new[] { "UserId", "TechnologyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_users_technologies_technologies_TechnologyId",
                schema: "user",
                table: "users_technologies",
                column: "TechnologyId",
                principalSchema: "user",
                principalTable: "technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_technologies_users_UserId",
                schema: "user",
                table: "users_technologies",
                column: "UserId",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_technologies_technologies_TechnologyId",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_users_technologies_users_UserId",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users_technologies",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.RenameTable(
                name: "users_technologies",
                schema: "user",
                newName: "UserTechnology",
                newSchema: "user");

            migrationBuilder.RenameIndex(
                name: "IX_users_technologies_TechnologyId",
                schema: "user",
                table: "UserTechnology",
                newName: "IX_UserTechnology_TechnologyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTechnology",
                schema: "user",
                table: "UserTechnology",
                columns: new[] { "UserId", "TechnologyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserTechnology_technologies_TechnologyId",
                schema: "user",
                table: "UserTechnology",
                column: "TechnologyId",
                principalSchema: "user",
                principalTable: "technologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTechnology_users_UserId",
                schema: "user",
                table: "UserTechnology",
                column: "UserId",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
