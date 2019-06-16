using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class HeavensLadder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    modename = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameModes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    teamname = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameModeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.id);
                    table.ForeignKey(
                        name: "FK__Challenge__gameM__5FB337D6",
                        column: x => x.gameModeId,
                        principalTable: "GameModes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    teamid = table.Column<int>(nullable: false),
                    gamemodeid = table.Column<int>(nullable: false),
                    rank = table.Column<int>(nullable: false),
                    wins = table.Column<int>(nullable: false),
                    losses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.id);
                    table.ForeignKey(
                        name: "FK__Rank__gamemodeid__5DCAEF64",
                        column: x => x.gamemodeid,
                        principalTable: "GameModes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Rank__teamid__5EBF139D",
                        column: x => x.teamid,
                        principalTable: "Team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectMessage",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    messagetime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    sendid = table.Column<int>(nullable: false),
                    recieveid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessage", x => x.id);
                    table.ForeignKey(
                        name: "FK__DirectMes__recie__5535A963",
                        column: x => x.recieveid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__DirectMes__sendi__5441852A",
                        column: x => x.sendid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTeam",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    teamid = table.Column<int>(nullable: false),
                    userid = table.Column<int>(nullable: false),
                    leader = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeam", x => x.id);
                    table.ForeignKey(
                        name: "FK__UserTeam__teamid__4F7CD00D",
                        column: x => x.teamid,
                        principalTable: "Team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__UserTeam__userid__5070F446",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sides",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    challengeid = table.Column<int>(nullable: false),
                    teamid = table.Column<int>(nullable: false),
                    winreport = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sides", x => x.id);
                    table.ForeignKey(
                        name: "FK__Sides__challenge__5812160E",
                        column: x => x.challengeid,
                        principalTable: "Challenge",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Sides__teamid__59063A47",
                        column: x => x.teamid,
                        principalTable: "Team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_gameModeId",
                table: "Challenge",
                column: "gameModeId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_recieveid",
                table: "DirectMessage",
                column: "recieveid");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessage_sendid",
                table: "DirectMessage",
                column: "sendid");

            migrationBuilder.CreateIndex(
                name: "IX_Rank_gamemodeid",
                table: "Rank",
                column: "gamemodeid");

            migrationBuilder.CreateIndex(
                name: "IX_Rank_teamid",
                table: "Rank",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_Sides_challengeid",
                table: "Sides",
                column: "challengeid");

            migrationBuilder.CreateIndex(
                name: "IX_Sides_teamid",
                table: "Sides",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_teamid",
                table: "UserTeam",
                column: "teamid");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeam_userid",
                table: "UserTeam",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectMessage");

            migrationBuilder.DropTable(
                name: "Rank");

            migrationBuilder.DropTable(
                name: "Sides");

            migrationBuilder.DropTable(
                name: "UserTeam");

            migrationBuilder.DropTable(
                name: "Challenge");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "GameModes");
        }
    }
}
