using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CoffeeShops (Name, OpeningHours, Address) VALUES ('egndgd', '9-5 dsada','address')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
