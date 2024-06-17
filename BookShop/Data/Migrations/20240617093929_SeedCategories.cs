using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData
                (
                    table: "Categories",
                    column: "Name",
                    values: ["Akcija", "Drama", "Misterija", "Poezija", "Triler", "Istorija"]
                );

            //migrationBuilder.Sql("INSERT INTO Categories(Name) VALUES('Akcija')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData
                (
                    table: "Categories",
                    keyColumn: "Name",
                    keyValues: ["Akcija", "Drama", "Misterija", "Poezija", "Triler", "Istorija"]
                );
        }
    }
}
