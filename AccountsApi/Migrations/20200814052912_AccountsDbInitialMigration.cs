using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountsApi.Migrations
{
    public partial class AccountsDbInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "FirstName", "LastName", "Type" },
                values: new object[,]
                {
                    { 1, 41000m, "Matt", "Black", 0 },
                    { 2, 52000m, "John", "Smith", 1 },
                    { 3, 104000m, "Ben", "Aston", 2 },
                    { 4, 50000m, "Ben", "Smith", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
