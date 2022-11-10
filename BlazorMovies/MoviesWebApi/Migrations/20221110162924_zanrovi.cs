using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesWebApi.Migrations
{
    public partial class zanrovi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zanr",
                table: "Film");

            migrationBuilder.AlterColumn<decimal>(
                name: "Ulozeno",
                table: "Film",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Naslov",
                table: "Film",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZanrId",
                table: "Film",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_ZanrId",
                table: "Film",
                column: "ZanrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Zanr_ZanrId",
                table: "Film",
                column: "ZanrId",
                principalTable: "Zanr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Zanr_ZanrId",
                table: "Film");

            migrationBuilder.DropTable(
                name: "Zanr");

            migrationBuilder.DropIndex(
                name: "IX_Film_ZanrId",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "ZanrId",
                table: "Film");

            migrationBuilder.AlterColumn<decimal>(
                name: "Ulozeno",
                table: "Film",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<string>(
                name: "Naslov",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "Zanr",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
