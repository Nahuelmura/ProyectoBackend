using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPedido.Migrations
{
    /// <inheritdoc />
    public partial class Cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Pedidios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Pedidios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidios_ClienteID",
                table: "Pedidios",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidios_Clientes_ClienteID",
                table: "Pedidios",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidios_Clientes_ClienteID",
                table: "Pedidios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Pedidios_ClienteID",
                table: "Pedidios");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Pedidios");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Pedidios");
        }
    }
}
