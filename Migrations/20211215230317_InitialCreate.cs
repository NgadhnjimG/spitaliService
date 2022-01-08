using Microsoft.EntityFrameworkCore.Migrations;

namespace Spitali.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Departments = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "HospitalName",
                columns: table => new
                {
                    HospitalNameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalName", x => x.HospitalNameID);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Departments = table.Column<int>(type: "int", nullable: false),
                    Hospital = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK__Doctor__Departme__4F7CD00D",
                        column: x => x.Departments,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Doctor__Hospital__5070F446",
                        column: x => x.Hospital,
                        principalTable: "HospitalName",
                        principalColumn: "HospitalNameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spitals",
                columns: table => new
                {
                    SpitalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Departments = table.Column<int>(type: "int", nullable: false),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spitals", x => x.SpitalID);
                    table.ForeignKey(
                        name: "FK__Spitals__Departm__48CFD27E",
                        column: x => x.Departments,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Spitals__Hospita__47DBAE45",
                        column: x => x.HospitalName,
                        principalTable: "HospitalName",
                        principalColumn: "HospitalNameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Departments",
                table: "Doctor",
                column: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Hospital",
                table: "Doctor",
                column: "Hospital");

            migrationBuilder.CreateIndex(
                name: "IX_Spitals_Departments",
                table: "Spitals",
                column: "Departments");

            migrationBuilder.CreateIndex(
                name: "IX_Spitals_HospitalName",
                table: "Spitals",
                column: "HospitalName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Spitals");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "HospitalName");
        }
    }
}
