using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoundClinic_WPF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoleUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "ApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "NationalCode",
                keyValue: 1285046358L,
                column: "RoleId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_RoleId",
                table: "ApplicationUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_ApplicationRoles_RoleId",
                table: "ApplicationUsers",
                column: "RoleId",
                principalTable: "ApplicationRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_ApplicationRoles_RoleId",
                table: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_RoleId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ApplicationUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationRoleApplicationUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersNationalCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleApplicationUser", x => new { x.RolesId, x.UsersNationalCode });
                    table.ForeignKey(
                        name: "FK_ApplicationRoleApplicationUser_ApplicationRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleApplicationUser_ApplicationUsers_UsersNationalCode",
                        column: x => x.UsersNationalCode,
                        principalTable: "ApplicationUsers",
                        principalColumn: "NationalCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationRoleApplicationUser",
                columns: new[] { "RolesId", "UsersNationalCode" },
                values: new object[] { 1, 1285046358L });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleApplicationUser_UsersNationalCode",
                table: "ApplicationRoleApplicationUser",
                column: "UsersNationalCode");
        }
    }
}
