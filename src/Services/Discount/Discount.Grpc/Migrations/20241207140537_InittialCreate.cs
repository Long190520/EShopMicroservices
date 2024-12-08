using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Discount.Grpc.Migrations
{
    /// <inheritdoc />
    public partial class InittialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Couppons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couppons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Couppons",
                columns: new[] { "Id", "Amount", "Description", "ProductName" },
                values: new object[,]
                {
                    { 1, 20, "Book couppon", "Book" },
                    { 2, 5, "Pen couppon", "Pen" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Couppons");
        }
    }
}
