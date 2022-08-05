using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "UsrCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsrFilterNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsrFilterNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsrFilterNames_UsrCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "UsrCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsrFilterValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterNameId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsrFilterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsrFilterValues_UsrFilterNames_FilterNameId",
                        column: x => x.FilterNameId,
                        principalTable: "UsrFilterNames",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsrProductsFilterValues",
                columns: table => new
                {
                    FilterValuesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsrProductsFilterValues", x => new { x.FilterValuesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_UsrProductsFilterValues_UsrFilterValues_FilterValuesId",
                        column: x => x.FilterValuesId,
                        principalTable: "UsrFilterValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsrProductsFilterValues_UsrProducts_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "UsrProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsrFilterNames_CategoryId",
                table: "UsrFilterNames",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UsrFilterValues_FilterNameId",
                table: "UsrFilterValues",
                column: "FilterNameId");

            migrationBuilder.CreateIndex(
                name: "IX_UsrProductsFilterValues_ProductsId",
                table: "UsrProductsFilterValues",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsrProductsFilterValues");

            migrationBuilder.DropTable(
                name: "UsrFilterValues");

            migrationBuilder.DropTable(
                name: "UsrFilterNames");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "UsrCategories");
        }
    }
}
