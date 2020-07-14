using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Minos.Engine.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminGroup",
                columns: table => new
                {
                    AG_Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AG_Name = table.Column<string>(maxLength: 20, nullable: false),
                    AG_Level = table.Column<int>(nullable: false),
                    AG_Description = table.Column<string>(maxLength: 100, nullable: true),
                    AG_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    AG_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminGroup", x => x.AG_Id);
                });

            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    C_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    C_Category = table.Column<string>(maxLength: 50, nullable: false),
                    C_Text = table.Column<string>(maxLength: 20, nullable: false),
                    C_Value = table.Column<string>(maxLength: 20, nullable: false),
                    C_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    C_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.C_Idx);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    C_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    C_Id = table.Column<string>(maxLength: 50, nullable: false),
                    C_Name = table.Column<string>(maxLength: 20, nullable: false),
                    C_email = table.Column<string>(maxLength: 100, nullable: false),
                    C_Phone = table.Column<string>(maxLength: 20, nullable: false),
                    C_UserMaxCount = table.Column<int>(nullable: false),
                    C_DBVersion = table.Column<string>(maxLength: 20, nullable: false),
                    C_DisplayFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    C_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    C_DeleteFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    C_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.C_Idx);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    A_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    A_AG_Id = table.Column<int>(nullable: false),
                    A_Id = table.Column<string>(maxLength: 50, nullable: false),
                    A_Password = table.Column<string>(maxLength: 50, nullable: false),
                    A_Name = table.Column<string>(maxLength: 20, nullable: false),
                    A_email = table.Column<string>(maxLength: 100, nullable: false),
                    A_Phone = table.Column<string>(maxLength: 20, nullable: false),
                    A_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    A_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.A_Idx);
                    table.ForeignKey(
                        name: "FK_Admin_AdminGroup_A_AG_Id",
                        column: x => x.A_AG_Id,
                        principalTable: "AdminGroup",
                        principalColumn: "AG_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminMenu",
                columns: table => new
                {
                    AM_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AM_AG_Id = table.Column<int>(nullable: false),
                    AM_Up_Idx = table.Column<int>(nullable: false),
                    AM_Ordered = table.Column<int>(nullable: false),
                    AM_Title = table.Column<string>(maxLength: 50, nullable: false),
                    AM_Link = table.Column<string>(maxLength: 100, nullable: false),
                    AM_Target = table.Column<string>(maxLength: 100, nullable: false),
                    AM_DisplayFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    AM_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    AM_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMenu", x => x.AM_Idx);
                    table.ForeignKey(
                        name: "FK_AdminMenu_AdminGroup_AM_AG_Id",
                        column: x => x.AM_AG_Id,
                        principalTable: "AdminGroup",
                        principalColumn: "AG_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    D_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    C_Idx = table.Column<int>(nullable: false),
                    D_Mac = table.Column<string>(maxLength: 20, nullable: false),
                    D_Firmware = table.Column<string>(maxLength: 50, nullable: false),
                    D_FirmwareVersion = table.Column<int>(nullable: false),
                    D_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    D_DeleteFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    D_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.D_Idx);
                    table.ForeignKey(
                        name: "FK_Device_Company_C_Idx",
                        column: x => x.C_Idx,
                        principalTable: "Company",
                        principalColumn: "C_Idx",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    U_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    U_C_Id = table.Column<int>(nullable: false),
                    U_Id = table.Column<string>(maxLength: 50, nullable: false),
                    U_Name = table.Column<string>(maxLength: 20, nullable: false),
                    U_Phone = table.Column<string>(maxLength: 20, nullable: false),
                    U_Email = table.Column<string>(maxLength: 100, nullable: false),
                    U_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    U_DeleteFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    U_InsDate = table.Column<DateTime>(nullable: false),
                    U_UptDate = table.Column<DateTime>(nullable: false),
                    A_UptIdx = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.U_Idx);
                    table.ForeignKey(
                        name: "FK_User_Admin_A_UptIdx",
                        column: x => x.A_UptIdx,
                        principalTable: "Admin",
                        principalColumn: "A_Idx",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Company_U_C_Id",
                        column: x => x.U_C_Id,
                        principalTable: "Company",
                        principalColumn: "C_Idx",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UG_Idx = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UG_C_Idx = table.Column<int>(nullable: false),
                    UG_U_Idx = table.Column<int>(nullable: false),
                    UG_level = table.Column<int>(nullable: false),
                    UG_UseFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    UG_DeleteFlag = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    UG_InsDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UG_Idx);
                    table.ForeignKey(
                        name: "FK_UserGroup_Company_UG_C_Idx",
                        column: x => x.UG_C_Idx,
                        principalTable: "Company",
                        principalColumn: "C_Idx",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UG_U_Idx",
                        column: x => x.UG_U_Idx,
                        principalTable: "User",
                        principalColumn: "U_Idx",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_A_AG_Id",
                table: "Admin",
                column: "A_AG_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AdminMenu_AM_AG_Id",
                table: "AdminMenu",
                column: "AM_AG_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Device_C_Idx",
                table: "Device",
                column: "C_Idx");

            migrationBuilder.CreateIndex(
                name: "IX_User_A_UptIdx",
                table: "User",
                column: "A_UptIdx");

            migrationBuilder.CreateIndex(
                name: "IX_User_U_C_Id",
                table: "User",
                column: "U_C_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UG_C_Idx",
                table: "UserGroup",
                column: "UG_C_Idx");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UG_U_Idx",
                table: "UserGroup",
                column: "UG_U_Idx");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminMenu");

            migrationBuilder.DropTable(
                name: "Code");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "AdminGroup");
        }
    }
}
