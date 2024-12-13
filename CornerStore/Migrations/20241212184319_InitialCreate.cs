using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CornerStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cashiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CashierID = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidOnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cashiers_CashierID",
                        column: x => x.CashierID,
                        principalTable: "Cashiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cashiers",
                columns: new[] { "Id", "FirstName", "FullName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "John Doe", "Doe" },
                    { 2, "Jane", "Jane Smith", "Smith" },
                    { 3, "Alex", "Alex Taylor", "Taylor" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Beverage" },
                    { 2, "Meat and Deli" },
                    { 3, "Fresh Produce" },
                    { 4, "Tobacco Products" },
                    { 5, "Snacks" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CashierID", "PaidOnDate", "Total" },
                values: new object[,]
                {
                    { 101, 1, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.75m },
                    { 102, 1, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 25.00m },
                    { 201, 2, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.25m },
                    { 202, 2, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.00m },
                    { 203, 2, null, 12.50m },
                    { 301, 3, new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.00m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CategoryId", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "Coca-Cola", 1, 1.50m, "Coca-Cola" },
                    { 2, "Coca-Cola", 1, 1.50m, "Sprite" },
                    { 3, "DeliFresh", 2, 5.99m, "Turkey Sandwich" },
                    { 4, "Fresh Farm", 3, 0.25m, "Banana" },
                    { 5, "Marlboro", 4, 12.00m, "Marlboro Gold Pack" },
                    { 6, "Lays", 5, 1.75m, "Lays Classic Chips" },
                    { 7, "KitKat", 5, 1.25m, "KitKat Chocolate Bar" },
                    { 8, "Tropicana", 1, 2.50m, "Orange Juice" },
                    { 9, "DeliFresh", 2, 5.99m, "Ham Sandwich" },
                    { 10, "Fresh Farm", 3, 0.50m, "Apple" },
                    { 11, "Pepsi", 1, 1.50m, "Pepsi" },
                    { 12, "Aquafina", 1, 1.00m, "Aquafina Water" },
                    { 13, "Jack Link's", 5, 3.99m, "Beef Jerky" },
                    { 14, "DeliFresh", 2, 4.99m, "Egg Sandwich" },
                    { 15, "Fresh Farm", 3, 0.30m, "Carrot" },
                    { 16, "Camel", 4, 11.50m, "Camel Blue Pack" },
                    { 17, "Snickers", 5, 1.25m, "Snickers Bar" },
                    { 18, "Red Bull", 1, 2.99m, "Energy Drink" },
                    { 19, "Oscar Mayer", 2, 2.50m, "Hot Dog" },
                    { 20, "Fresh Farm", 3, 0.20m, "Potato" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 101, 1, 2 },
                    { 101, 3, 1 },
                    { 102, 6, 3 },
                    { 102, 7, 2 },
                    { 201, 4, 6 },
                    { 201, 9, 1 },
                    { 202, 2, 2 },
                    { 202, 5, 1 },
                    { 203, 13, 1 },
                    { 203, 17, 4 },
                    { 301, 18, 2 },
                    { 301, 19, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierID",
                table: "Orders",
                column: "CashierID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cashiers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
