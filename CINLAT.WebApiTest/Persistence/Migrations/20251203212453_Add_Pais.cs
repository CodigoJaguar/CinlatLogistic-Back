using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CINLAT.WebApiTest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Pais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    cca3 = table.Column<string>(type: "TEXT", nullable: false),
                    commonName = table.Column<string>(type: "TEXT", nullable: false),
                    officialName = table.Column<string>(type: "TEXT", nullable: false),
                    independent = table.Column<bool>(type: "INTEGER", nullable: false),
                    area = table.Column<double>(type: "REAL", nullable: false),
                    population = table.Column<long>(type: "INTEGER", nullable: false),
                    flag = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.cca3);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
