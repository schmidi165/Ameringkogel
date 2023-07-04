using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(3)", maxLength: 255, precision: 3, nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(3)", maxLength: 255, precision: 3, nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(3)", maxLength: 255, precision: 3, nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "WorkItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    MinStartDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    EstimatedTimeEffort = table.Column<TimeSpan>(type: "time(3)", precision: 3, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(3)", maxLength: 255, precision: 3, nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItem_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_UserGroupId",
                table: "User",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItem_UserId",
                table: "WorkItem",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "WorkItem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserGroup");
        }
    }
}
