using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesWebApi.Migrations
{
    public partial class dodatkompozitnikljuczakorisnikepretplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Potroseno = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pretplata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iznos = table.Column<decimal>(type: "money", nullable: false),
                    DatumIsteka = table.Column<DateTime>(type: "date", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    PaketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pretplata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pretplata_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pretplata_Paket_PaketId",
                        column: x => x.PaketId,
                        principalTable: "Paket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pretplata_KorisnikId",
                table: "Pretplata",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Pretplata_PaketId",
                table: "Pretplata",
                column: "PaketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pretplata");

            migrationBuilder.DropTable(
                name: "Korisnik");
        }
    }
}
