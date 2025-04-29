using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "product_name",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "product_type_id",
                table: "products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "product_names",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_names", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_products_product_type_id",
                table: "products",
                column: "product_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_product_types_product_type_id",
                table: "products",
                column: "product_type_id",
                principalTable: "product_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_product_types_product_type_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "product_names");

            migrationBuilder.DropTable(
                name: "product_types");

            migrationBuilder.DropIndex(
                name: "ix_products_product_type_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_name",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_type_id",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "products",
                type: "text",
                nullable: true);
        }
    }
}
