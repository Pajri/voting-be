using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "votings",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    voters_count = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votings", x => x.ID);
                    table.ForeignKey(
                        name: "fk_voting_category",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVoting",
                columns: table => new
                {
                    VotersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotingsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoting", x => new { x.VotersID, x.VotingsID });
                    table.ForeignKey(
                        name: "FK_UserVoting_users_VotersID",
                        column: x => x.VotersID,
                        principalTable: "users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoting_votings_VotingsID",
                        column: x => x.VotingsID,
                        principalTable: "votings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVoting_VotingsID",
                table: "UserVoting",
                column: "VotingsID");

            migrationBuilder.CreateIndex(
                name: "IX_votings_category_id",
                table: "votings",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVoting");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "votings");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
