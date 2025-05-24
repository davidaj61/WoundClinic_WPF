using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoundClinic_WPF.Migrations
{
    /// <inheritdoc />
    public partial class add_Atba_add_LastLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAtba",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "ApplicationUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "NationalCode",
                keyValue: 1285046358L,
                column: "LastLogin",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "NationalCode",
                keyValue: 1285046358L,
                column: "IsAtba",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAtba",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "ApplicationUsers");
        }
    }
}
