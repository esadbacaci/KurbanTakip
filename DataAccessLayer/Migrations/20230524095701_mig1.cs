using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Carikarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdSoyad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefon = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Referans = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Aciklama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Il = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ilce = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ulke = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carikarts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stoks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HisseAdet = table.Column<int>(type: "int", nullable: false),
                    HisseFiyat = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Kilo = table.Column<double>(type: "double", nullable: false),
                    Yas = table.Column<int>(type: "int", nullable: false),
                    KupeNo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cins = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoks", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Hissecarikarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HisseTutar = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    VekaletAlindiMi = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CariKartId = table.Column<int>(type: "int", nullable: false),
                    StokId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hissecarikarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hissecarikarts_Carikarts_CariKartId",
                        column: x => x.CariKartId,
                        principalTable: "Carikarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hissecarikarts_Stoks_StokId",
                        column: x => x.StokId,
                        principalTable: "Stoks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cariislems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CariKartId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Borc = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Alacak = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Aciklama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HisseCariKartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cariislems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cariislems_Carikarts_CariKartId",
                        column: x => x.CariKartId,
                        principalTable: "Carikarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cariislems_Hissecarikarts_HisseCariKartId",
                        column: x => x.HisseCariKartId,
                        principalTable: "Hissecarikarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kasas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Aciklama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CariIslemId = table.Column<int>(type: "int", nullable: false),
                    GirisMi = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kasas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kasas_Cariislems_CariIslemId",
                        column: x => x.CariIslemId,
                        principalTable: "Cariislems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cariislems_CariKartId",
                table: "Cariislems",
                column: "CariKartId");

            migrationBuilder.CreateIndex(
                name: "IX_Cariislems_HisseCariKartId",
                table: "Cariislems",
                column: "HisseCariKartId");

            migrationBuilder.CreateIndex(
                name: "IX_Hissecarikarts_CariKartId",
                table: "Hissecarikarts",
                column: "CariKartId");

            migrationBuilder.CreateIndex(
                name: "IX_Hissecarikarts_StokId",
                table: "Hissecarikarts",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_Kasas_CariIslemId",
                table: "Kasas",
                column: "CariIslemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kasas");

            migrationBuilder.DropTable(
                name: "Cariislems");

            migrationBuilder.DropTable(
                name: "Hissecarikarts");

            migrationBuilder.DropTable(
                name: "Carikarts");

            migrationBuilder.DropTable(
                name: "Stoks");
        }
    }
}
