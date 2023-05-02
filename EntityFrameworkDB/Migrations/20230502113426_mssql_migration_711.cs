using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkDB.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_711 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Token = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    FIRST_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    LAST_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STARTDATA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ENDDATA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vacancy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    StartDate = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EndDate = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TestColoumn = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UseridUser = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Vacancy_users_UseridUser",
                        column: x => x.UseridUser,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancy_UseridUser",
                table: "Vacancy",
                column: "UseridUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "Vacancy");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
