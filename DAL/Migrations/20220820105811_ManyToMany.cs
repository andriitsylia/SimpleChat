using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Talks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.CreateTable(
                name: "MemberTalk",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    TalksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTalk", x => new { x.MembersId, x.TalksId });
                    table.ForeignKey(
                        name: "FK_MemberTalk_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberTalk_Talks_TalksId",
                        column: x => x.TalksId,
                        principalTable: "Talks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberTalk_TalksId",
                table: "MemberTalk",
                column: "TalksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberTalk");

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
        }
    }
}
