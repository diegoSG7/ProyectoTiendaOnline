using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoTiendaOnline.Migrations
{
    public partial class FacturasVendedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Facturas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Facturas");
        }
    }
}
