using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoTiendaOnline.Migrations
{
    public partial class CompraDirecta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturaDetalles_Facturas_FacturaId",
                table: "FacturaDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_FacturaDetalles_Productos_ProductoId",
                table: "FacturaDetalles");

            migrationBuilder.DropIndex(
                name: "IX_FacturaDetalles_FacturaId",
                table: "FacturaDetalles");

            migrationBuilder.DropIndex(
                name: "IX_FacturaDetalles_ProductoId",
                table: "FacturaDetalles");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Facturas");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Facturas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Facturas",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Facturas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ProductoId",
                table: "Facturas",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Productos_ProductoId",
                table: "Facturas",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Productos_ProductoId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ProductoId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Facturas");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalles_FacturaId",
                table: "FacturaDetalles",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalles_ProductoId",
                table: "FacturaDetalles",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturaDetalles_Facturas_FacturaId",
                table: "FacturaDetalles",
                column: "FacturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacturaDetalles_Productos_ProductoId",
                table: "FacturaDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
