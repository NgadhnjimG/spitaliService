using Microsoft.EntityFrameworkCore.Migrations;

namespace Spitali.Migrations
{
    public partial class institution_updateded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitutionType",
                table: "HospitalName",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionType",
                table: "HospitalName");
        }
    }
}
