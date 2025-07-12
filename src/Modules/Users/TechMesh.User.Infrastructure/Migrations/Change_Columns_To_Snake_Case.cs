using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMesh.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_Columns_To_Snake_Case : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_technologies_technologies_TechnologyId",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_users_technologies_users_UserId",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.RenameColumn(
                name: "TechnologyId",
                schema: "user",
                table: "users_technologies",
                newName: "technology_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "user",
                table: "users_technologies",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_technologies_TechnologyId",
                schema: "user",
                table: "users_technologies",
                newName: "IX_users_technologies_technology_id");

            migrationBuilder.RenameColumn(
                name: "Street",
                schema: "user",
                table: "users",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "user",
                table: "users",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "user",
                table: "users",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Number",
                schema: "user",
                table: "users",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                schema: "user",
                table: "users",
                newName: "neighborhood");

            migrationBuilder.RenameColumn(
                name: "Level",
                schema: "user",
                table: "users",
                newName: "level");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "user",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Country",
                schema: "user",
                table: "users",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Complement",
                schema: "user",
                table: "users",
                newName: "complement");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "user",
                table: "users",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "user",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "user",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "user",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                schema: "user",
                table: "users",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "user",
                table: "users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "user",
                table: "users",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "user",
                table: "users",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "user",
                table: "technologies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "user",
                table: "technologies",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "user",
                table: "technologies",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "user",
                table: "technologies",
                newName: "created_at");

            migrationBuilder.AddForeignKey(
                name: "FK_users_technologies_technologies_technology_id",
                schema: "user",
                table: "users_technologies",
                column: "technology_id",
                principalSchema: "user",
                principalTable: "technologies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_technologies_users_user_id",
                schema: "user",
                table: "users_technologies",
                column: "user_id",
                principalSchema: "user",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_technologies_technologies_technology_id",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_users_technologies_users_user_id",
                schema: "user",
                table: "users_technologies");

            migrationBuilder.RenameColumn(
                name: "technology_id",
                schema: "user",
                table: "users_technologies",
                newName: "TechnologyId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "user",
                table: "users_technologies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_users_technologies_technology_id",
                schema: "user",
                table: "users_technologies",
                newName: "IX_users_technologies_TechnologyId");

            migrationBuilder.RenameColumn(
                name: "street",
                schema: "user",
                table: "users",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "user",
                table: "users",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "state",
                schema: "user",
                table: "users",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "number",
                schema: "user",
                table: "users",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "neighborhood",
                schema: "user",
                table: "users",
                newName: "Neighborhood");

            migrationBuilder.RenameColumn(
                name: "level",
                schema: "user",
                table: "users",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "user",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "country",
                schema: "user",
                table: "users",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "complement",
                schema: "user",
                table: "users",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "user",
                table: "users",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "user",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                schema: "user",
                table: "users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                schema: "user",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                schema: "user",
                table: "users",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                schema: "user",
                table: "users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "full_name",
                schema: "user",
                table: "users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                schema: "user",
                table: "users",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "user",
                table: "technologies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "user",
                table: "technologies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                schema: "user",
                table: "technologies",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                schema: "user",
                table: "technologies",
                newName: "CreatedAt");

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
    }
}
