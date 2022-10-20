using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkJob", x => x.JobId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModuleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubModuleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ComponentSubModuleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkModule", x => x.ModuleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SiteName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkSite", x => x.SiteId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkBank", x => x.BankId);
                    table.ForeignKey(
                        name: "fkBank_Site",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Workday = table.Column<int>(type: "int", nullable: false),
                    IdCard = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Birthdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    BackAccountNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FireDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkEmployee", x => x.Workday);
                    table.ForeignKey(
                        name: "fkEmployee_Bank",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId");
                    table.ForeignKey(
                        name: "fkEmployee_JobTitle",
                        column: x => x.JobId,
                        principalTable: "JobTitles",
                        principalColumn: "JobId");
                    table.ForeignKey(
                        name: "fkEmployee_Site",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Workday = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExp = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkUser", x => x.Workday);
                    table.ForeignKey(
                        name: "fkUser_Employee",
                        column: x => x.Workday,
                        principalTable: "Employees",
                        principalColumn: "Workday",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkRole", x => x.RoleId);
                    table.ForeignKey(
                        name: "fkRole_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Workday",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fkRole_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Workday");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    View = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Create = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Edit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Delete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pkPolicy", x => x.PolicyId);
                    table.ForeignKey(
                        name: "fkModule_Policy",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId");
                    table.ForeignKey(
                        name: "fkRole_Policy",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "JobTitles",
                columns: new[] { "JobId", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, true, "Designer" },
                    { 2, true, "Supervisor" },
                    { 3, true, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "ModuleId", "ComponentSubModuleName", "IsActive", "ModuleName", "SubModuleName" },
                values: new object[,]
                {
                    { 1, "employee", true, "payroll", "Employee" },
                    { 2, "incident", true, "payroll", "Incident" },
                    { 3, "incidentApproval", true, "payroll", "Incident Approval" },
                    { 4, "user", true, "security", "User" },
                    { 5, "role", true, "security", "Role" }
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "SiteId", "IsActive", "SiteName" },
                values: new object[,]
                {
                    { 1, true, "Costa Rica - San Jose" },
                    { 2, true, "Mexico - Mexicali" },
                    { 3, true, "China - " },
                    { 4, true, "Spain - Madrid" }
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "BankName", "IsActive", "SiteId" },
                values: new object[] { 1, "Banco de Costa Rica", true, 1 });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "BankName", "IsActive", "SiteId" },
                values: new object[] { 2, "Banco Nacional de Costa Rica", true, 1 });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "BankName", "IsActive", "SiteId" },
                values: new object[] { 3, "Banco BAC San Jose S.A.", true, 1 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Workday", "BackAccountNumber", "BankId", "Birthdate", "FireDate", "FirstName", "HireDate", "IdCard", "IsActive", "JobId", "LastName", "Salary", "SiteId", "SupervisorId" },
                values: new object[] { 1001, null, 1, new DateTime(2022, 10, 14, 20, 27, 21, 510, DateTimeKind.Utc).AddTicks(2880), null, "Esteban", new DateTime(2022, 10, 14, 20, 27, 21, 510, DateTimeKind.Utc).AddTicks(2884), 102220333, true, 1, "Fernandez", 9000.00m, 1, 1001 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Workday", "Email", "IsActive", "Password", "RefreshToken", "RefreshTokenExp", "Username" },
                values: new object[] { 1001, "esteban@hotmail.com", true, "lz2ZrEsIER3XoE79aWAi2kAi5CckccEK90P4Bxvjoc8=", null, null, "esteban.fernandez" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedBy", "CreationDate", "Description", "IsActive", "RoleName", "UpdateDate", "UpdatedBy" },
                values: new object[] { 1, 1001, new DateTime(2022, 10, 14, 14, 27, 21, 513, DateTimeKind.Local).AddTicks(4414), "root access", true, "root", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_SiteId",
                table: "Banks",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BankId",
                table: "Employees",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SiteId",
                table: "Employees",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ModuleId",
                table: "Policies",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_RoleId",
                table: "Policies",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedBy",
                table: "Roles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedBy",
                table: "Roles",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
