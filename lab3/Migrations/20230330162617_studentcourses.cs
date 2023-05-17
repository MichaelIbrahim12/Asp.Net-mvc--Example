using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab3.Migrations
{
    /// <inheritdoc />
    public partial class studentcourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoursesCrs_Id",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Crs_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Crs_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lect_Hours = table.Column<int>(type: "int", nullable: false),
                    Lab_Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Crs_Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CrsId = table.Column<int>(type: "int", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.StudentId, x.CrsId });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Courses",
                        principalColumn: "Crs_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CoursesCrs_Id",
                table: "Departments",
                column: "CoursesCrs_Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CrsId",
                table: "StudentCourses",
                column: "CrsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Courses_CoursesCrs_Id",
                table: "Departments",
                column: "CoursesCrs_Id",
                principalTable: "Courses",
                principalColumn: "Crs_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Courses_CoursesCrs_Id",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CoursesCrs_Id",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CoursesCrs_Id",
                table: "Departments");
        }
    }
}
