using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab3.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Students",
                newName: "deptId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_DepartmentID",
                table: "Students",
                newName: "IX_Students_deptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_deptId",
                table: "Students",
                column: "deptId",
                principalTable: "Departments",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_deptId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "deptId",
                table: "Students",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_deptId",
                table: "Students",
                newName: "IX_Students_DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentID",
                table: "Students",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "ID");
        }
    }
}
