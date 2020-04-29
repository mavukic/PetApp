using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetApp.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skloniste",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(maxLength: 70, nullable: false),
                    Grad = table.Column<string>(maxLength: 30, nullable: false),
                    Tel = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Web = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skloniste", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Udruga",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(maxLength: 70, nullable: false),
                    Grad = table.Column<string>(maxLength: 30, nullable: false),
                    Tel = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Web = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Udruga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ljubimac",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(maxLength: 30, nullable: false),
                    Opis = table.Column<string>(maxLength: 150, nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Grad = table.Column<string>(nullable: false),
                    SklonisteId = table.Column<int>(nullable: true),
                    Mjesto = table.Column<string>(maxLength: 30, nullable: false),
                    Vrsta = table.Column<string>(nullable: true),
                    Godine = table.Column<int>(nullable: false),
                    Slika = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ljubimac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ljubimac_Skloniste_SklonisteId",
                        column: x => x.SklonisteId,
                        principalTable: "Skloniste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostSklonista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    SklonisteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSklonista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSklonista_Skloniste_SklonisteId",
                        column: x => x.SklonisteId,
                        principalTable: "Skloniste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostUdruge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    UdrugaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUdruge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostUdruge_Udruga_UdrugaId",
                        column: x => x.UdrugaId,
                        principalTable: "Udruga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posvajatelj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(maxLength: 50, nullable: false),
                    Mjesto = table.Column<string>(maxLength: 30, nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    BrMob = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    LjubimacId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posvajatelj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posvajatelj_Ljubimac_LjubimacId",
                        column: x => x.LjubimacId,
                        principalTable: "Ljubimac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ljubimac_SklonisteId",
                table: "Ljubimac",
                column: "SklonisteId");

            migrationBuilder.CreateIndex(
                name: "IX_PostSklonista_SklonisteId",
                table: "PostSklonista",
                column: "SklonisteId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUdruge_UdrugaId",
                table: "PostUdruge",
                column: "UdrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Posvajatelj_LjubimacId",
                table: "Posvajatelj",
                column: "LjubimacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostSklonista");

            migrationBuilder.DropTable(
                name: "PostUdruge");

            migrationBuilder.DropTable(
                name: "Posvajatelj");

            migrationBuilder.DropTable(
                name: "Udruga");

            migrationBuilder.DropTable(
                name: "Ljubimac");

            migrationBuilder.DropTable(
                name: "Skloniste");
        }
    }
}
