using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newUpdateForOrderItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "OrderItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "OrderItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "OrderItems",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "OrderItems");
        }
    }
}
