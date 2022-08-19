using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sender = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    TalkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Talks_TalkId",
                        column: x => x.TalkId,
                        principalTable: "Talks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TalkMembers",
                columns: table => new
                {
                    TalkId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalkMembers", x => new { x.TalkId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_TalkMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TalkMembers_Talks_TalkId",
                        column: x => x.TalkId,
                        principalTable: "Talks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "FirstName", "LastName", "NickName" },
                values: new object[,]
                {
                    { 1, "Andy", "Brown1", "Andy1" },
                    { 2, "Andy", "Brown2", "Andy2" },
                    { 3, "Andy", "Brown3", "Andy3" },
                    { 4, "Andy", "Brown4", "Andy4" },
                    { 5, "Andy", "Brown5", "Andy5" }
                });

            migrationBuilder.InsertData(
                table: "Talks",
                columns: new[] { "Id", "IsPrivate", "Name" },
                values: new object[,]
                {
                    { 1, true, "" },
                    { 2, true, "" },
                    { 3, true, "" },
                    { 4, true, "" },
                    { 5, true, "" },
                    { 6, false, "Wolfs" },
                    { 7, false, "Sheeps" }
                });

            migrationBuilder.InsertData(
                table: "TalkMembers",
                columns: new[] { "MemberId", "TalkId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 5, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 5 },
                    { 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MemberId",
                table: "Messages",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TalkId",
                table: "Messages",
                column: "TalkId");

            migrationBuilder.CreateIndex(
                name: "IX_TalkMembers_MemberId",
                table: "TalkMembers",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "TalkMembers");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Talks");
        }
    }
}
