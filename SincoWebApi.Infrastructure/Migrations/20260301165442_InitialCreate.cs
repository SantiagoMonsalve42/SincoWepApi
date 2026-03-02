using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SincoWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblEstadoPaquete",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEstadoPaquete", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "tblPrioridad",
                columns: table => new
                {
                    PrioridadId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPrioridad", x => x.PrioridadId);
                });

            migrationBuilder.CreateTable(
                name: "tblRepartidor",
                columns: table => new
                {
                    RepartidorId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRepartidor", x => x.RepartidorId);
                });

            migrationBuilder.CreateTable(
                name: "tblPaquete",
                columns: table => new
                {
                    PaqueteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodigoSeguimiento = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    PrioridadId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaquete", x => x.PaqueteId);
                    table.ForeignKey(
                        name: "FK_tblPaquete_tblEstadoPaquete_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "tblEstadoPaquete",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPaquete_tblPrioridad_PrioridadId",
                        column: x => x.PrioridadId,
                        principalTable: "tblPrioridad",
                        principalColumn: "PrioridadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPaqueteRepartidor",
                columns: table => new
                {
                    PaqueteRepartidorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepartidorId = table.Column<int>(type: "int", nullable: false),
                    PaqueteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPaqueteRepartidor", x => x.PaqueteRepartidorId);
                    table.ForeignKey(
                        name: "FK_tblPaqueteRepartidor_tblPaquete_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "tblPaquete",
                        principalColumn: "PaqueteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPaqueteRepartidor_tblRepartidor_RepartidorId",
                        column: x => x.RepartidorId,
                        principalTable: "tblRepartidor",
                        principalColumn: "RepartidorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tblEstadoPaquete",
                columns: new[] { "EstadoId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "En bodega" },
                    { 2, "Asignado" },
                    { 3, "Entregado" }
                });

            migrationBuilder.InsertData(
                table: "tblPrioridad",
                columns: new[] { "PrioridadId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Alta" },
                    { 2, "Media" },
                    { 3, "Baja" }
                });

            migrationBuilder.InsertData(
                table: "tblRepartidor",
                columns: new[] { "RepartidorId", "Apellido", "Nombre" },
                values: new object[,]
                {
                    { 1, "Monsalve", "Andres" },
                    { 2, "Chavez", "Juan" },
                    { 3, "Ortiz", "Esteban" },
                    { 4, "Quiñones", "Andres" },
                    { 5, "Quiroga", "Laura" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPaqueteRepartidor_PaqueteId",
                table: "tblPaqueteRepartidor",
                column: "PaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteRepartidor_RepartidorId",
                table: "tblPaqueteRepartidor",
                column: "RepartidorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaquete_EstadoId",
                table: "tblPaquete",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPaquete_PrioridadId",
                table: "tblPaquete",
                column: "PrioridadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPaqueteRepartidor");

            migrationBuilder.DropTable(
                name: "tblPaquete");

            migrationBuilder.DropTable(
                name: "tblRepartidor");

            migrationBuilder.DropTable(
                name: "tblEstadoPaquete");

            migrationBuilder.DropTable(
                name: "tblPrioridad");
        }
    }
}
