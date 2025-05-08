using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjPM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    IdKlienta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.IdKlienta);
                });

            migrationBuilder.CreateTable(
                name: "Samochody",
                columns: table => new
                {
                    IdSamochodu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RokProdukcji = table.Column<int>(type: "int", nullable: false),
                    Kolor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dostepnosc = table.Column<bool>(type: "bit", nullable: false),
                    PojSilnika = table.Column<int>(type: "int", nullable: false),
                    MocSilnika = table.Column<int>(type: "int", nullable: false),
                    CenaZaWypozyczenie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samochody", x => x.IdSamochodu);
                });

            migrationBuilder.CreateTable(
                name: "Wypozyczenia",
                columns: table => new
                {
                    IdWypozyczenia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKlienta = table.Column<int>(type: "int", nullable: false),
                    IdSamochodu = table.Column<int>(type: "int", nullable: false),
                    DataRozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZakonczenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kwota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wypozyczenia", x => x.IdWypozyczenia);
                    table.ForeignKey(
                        name: "FK_Wypozyczenia_Klienci_IdKlienta",
                        column: x => x.IdKlienta,
                        principalTable: "Klienci",
                        principalColumn: "IdKlienta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wypozyczenia_Samochody_IdSamochodu",
                        column: x => x.IdSamochodu,
                        principalTable: "Samochody",
                        principalColumn: "IdSamochodu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wypozyczenia_IdKlienta",
                table: "Wypozyczenia",
                column: "IdKlienta");

            migrationBuilder.CreateIndex(
                name: "IX_Wypozyczenia_IdSamochodu",
                table: "Wypozyczenia",
                column: "IdSamochodu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wypozyczenia");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Samochody");
        }
    }
}
