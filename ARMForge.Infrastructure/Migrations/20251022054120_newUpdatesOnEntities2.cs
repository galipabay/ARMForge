using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newUpdatesOnEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "PurchaseOrders",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCity",
                table: "PurchaseOrders",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Incoterms",
                table: "PurchaseOrders",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalVolume",
                table: "PurchaseOrders",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalWeight",
                table: "PurchaseOrders",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "PurchaseOrderItems",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "PurchaseOrderItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "PurchaseOrderItems",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "PurchaseOrderItems",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryCity",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Incoterms",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TotalVolume",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "PurchaseOrderItems");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PurchaseOrderItems");
        }
    }
}
