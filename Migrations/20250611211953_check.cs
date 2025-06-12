using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoundClinic_WPF.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthOFYear",
                table: "WoundCares",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthOFYear",
                table: "WoundCares");
        }
    }
}
