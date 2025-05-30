﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WoundClinic_WPF.Migrations
{
    /// <inheritdoc />
    public partial class change_dressing_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dressings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DressingName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HasConstPrice = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDrug = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dressings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    NationalCode = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.NationalCode);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    NationalCode = table.Column<long>(type: "bigint", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.NationalCode);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_Persons_NationalCode",
                        column: x => x.NationalCode,
                        principalTable: "Persons",
                        principalColumn: "NationalCode");
                });

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

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    NationalCode = table.Column<long>(type: "bigint", nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.NationalCode);
                    table.ForeignKey(
                        name: "FK_Patients_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "NationalCode");
                    table.ForeignKey(
                        name: "FK_Patients_Persons_NationalCode",
                        column: x => x.NationalCode,
                        principalTable: "Persons",
                        principalColumn: "NationalCode");
                });

            migrationBuilder.CreateTable(
                name: "WoundCares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundCares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WoundCares_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "NationalCode");
                    table.ForeignKey(
                        name: "FK_WoundCares_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "NationalCode");
                });

            migrationBuilder.CreateTable(
                name: "DressingCares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundCareId = table.Column<int>(type: "int", nullable: false),
                    DressingId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DressingCares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DressingCares_Dressings_DressingId",
                        column: x => x.DressingId,
                        principalTable: "Dressings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DressingCares_WoundCares_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "WoundCares",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ApplicationRoles",
                columns: new[] { "Id", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "مدیر سیستم", "admin" },
                    { 2, "سوپروایزور", "Supervisor" },
                    { 3, "کاربر", "user" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "NationalCode", "FirstName", "Gender", "LastName" },
                values: new object[] { 1285046358L, "داود", true, "اقاویل جهرمی" });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "NationalCode", "IsActive", "PasswordHash" },
                values: new object[] { 1285046358L, true, "18bd4ebd1a9436142d16224c33327a9b8323ac8949af256d1c37930c6308b2db" });

            migrationBuilder.InsertData(
                table: "ApplicationRoleApplicationUser",
                columns: new[] { "RolesId", "UsersNationalCode" },
                values: new object[] { 1, 1285046358L });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleApplicationUser_UsersNationalCode",
                table: "ApplicationRoleApplicationUser",
                column: "UsersNationalCode");

            migrationBuilder.CreateIndex(
                name: "IX_DressingCares_DressingId",
                table: "DressingCares",
                column: "DressingId");

            migrationBuilder.CreateIndex(
                name: "IX_DressingCares_WoundCareId",
                table: "DressingCares",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WoundCares_PatientId",
                table: "WoundCares",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_WoundCares_UserId",
                table: "WoundCares",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleApplicationUser");

            migrationBuilder.DropTable(
                name: "DressingCares");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "Dressings");

            migrationBuilder.DropTable(
                name: "WoundCares");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
