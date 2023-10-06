using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class Agregandodetalles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "medicamento_proveedor",
                columns: new[] { "MedicamentoId", "ProveedorId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "medicamento_proveedor",
                keyColumns: new[] { "MedicamentoId", "ProveedorId" },
                keyValues: new object[] { 1, 1 });
        }
    }
}
