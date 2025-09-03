using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForDriverEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Drivers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Drivers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInspectionDate",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LastInspectionDate",
                table: "Drivers");
        }
    }
}
