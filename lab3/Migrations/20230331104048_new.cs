using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab3.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Courses_CoursesCrs_Id",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CoursesCrs_Id",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CoursesCrs_Id",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "CoursesDepartment",
                columns: table => new
                {
                    CoursesCrs_Id = table.Column<int>(type: "int", nullable: false),
                    DepartmentsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesDepartment", x => new { x.CoursesCrs_Id, x.DepartmentsID });
                    table.ForeignKey(
                        name: "FK_CoursesDepartment_Courses_CoursesCrs_Id",
                        column: x => x.CoursesCrs_Id,
                        principalTable: "Courses",
                        principalColumn: "Crs_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesDepartment_Departments_DepartmentsID",
                        column: x => x.DepartmentsID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesDepartment_DepartmentsID",
                table: "CoursesDepartment",
                column: "DepartmentsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesDepartment");

            migrationBuilder.AddColumn<int>(
                name: "CoursesCrs_Id",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CoursesCrs_Id",
                table: "Departments",
                column: "CoursesCrs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Courses_CoursesCrs_Id",
                table: "Departments",
                column: "CoursesCrs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id");
        }
    }
}
