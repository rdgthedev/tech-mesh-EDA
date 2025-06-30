using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMesh.User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_DeletedAt_Column_To_Snake_Case : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                schema: "user",
                table: "users",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                schema: "user",
                table: "technologies",
                newName: "deleted_at");

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at",
                schema: "user",
                table: "users",
                type: "TIMESTAMP",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at",
                schema: "user",
                table: "technologies",
                type: "TIMESTAMP",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deleted_at",
                schema: "user",
                table: "users",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                schema: "user",
                table: "technologies",
                newName: "DeletedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                schema: "user",
                table: "users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                schema: "user",
                table: "technologies",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP",
                oldNullable: true);
        }
    }
}
