using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGPA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "gpa",
                table: "tb_m_educations",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "gpa",
                table: "tb_m_educations",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
