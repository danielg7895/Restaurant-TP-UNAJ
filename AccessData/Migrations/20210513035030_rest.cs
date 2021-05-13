using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccessData.Migrations
{
    public partial class rest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntregas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntregas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercaderias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMercaderias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaEntregaId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comandas_FormaEntregas_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntregas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mercaderias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoMercaderiaId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Preparation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercaderias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mercaderias_TipoMercaderias_TipoMercaderiaId",
                        column: x => x.TipoMercaderiaId,
                        principalTable: "TipoMercaderias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComandaMercaderias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercaderiaId = table.Column<int>(type: "int", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaMercaderias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderias_Comandas_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaMercaderias_Mercaderias_MercaderiaId",
                        column: x => x.MercaderiaId,
                        principalTable: "Mercaderias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FormaEntregas",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Salon" },
                    { 2, "Delivery" },
                    { 3, "Pedidos Ya" }
                });

            migrationBuilder.InsertData(
                table: "TipoMercaderias",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Platos" },
                    { 2, "Bebidas" },
                    { 3, "Postres" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderias_ComandaId",
                table: "ComandaMercaderias",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaMercaderias_MercaderiaId",
                table: "ComandaMercaderias",
                column: "MercaderiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_FormaEntregaId",
                table: "Comandas",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercaderias_TipoMercaderiaId",
                table: "Mercaderias",
                column: "TipoMercaderiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaMercaderias");

            migrationBuilder.DropTable(
                name: "Comandas");

            migrationBuilder.DropTable(
                name: "Mercaderias");

            migrationBuilder.DropTable(
                name: "FormaEntregas");

            migrationBuilder.DropTable(
                name: "TipoMercaderias");
        }
    }
}
