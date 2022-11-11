using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesWebApi.Migrations
{
    public partial class dodatipaketifilmova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumFormiranja = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmPaket",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    PaketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmPaket", x => new { x.FilmId, x.PaketId });
                    table.ForeignKey(
                        name: "FK_FilmPaket_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmPaket_Paket_PaketId",
                        column: x => x.PaketId,
                        principalTable: "Paket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmPaket_PaketId",
                table: "FilmPaket",
                column: "PaketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmPaket");

            migrationBuilder.DropTable(
                name: "Paket");
        }
    }
}
