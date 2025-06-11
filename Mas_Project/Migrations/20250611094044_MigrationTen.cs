using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mas_Project.Migrations
{
    /// <inheritdoc />
    public partial class MigrationTen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ExperienceReward = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.AchievementID);
                });

            migrationBuilder.CreateTable(
                name: "QuestBoards",
                columns: table => new
                {
                    QuestBoardID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestBoards", x => x.QuestBoardID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaxNumberOfPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReputationScore = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildMembers",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    ExperiencePoints = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberRole = table.Column<int>(type: "INTEGER", nullable: false),
                    Relic = table.Column<string>(type: "TEXT", nullable: true),
                    LeadershipStyle = table.Column<string>(type: "TEXT", nullable: true),
                    AssignedRoom = table.Column<int>(type: "INTEGER", nullable: true),
                    TeamGuid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMembers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_GuildMembers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    QuestID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    MaxNumberOfParticipants = table.Column<int>(type: "INTEGER", nullable: false),
                    MinRank = table.Column<int>(type: "INTEGER", nullable: false),
                    DurationHours = table.Column<double>(type: "REAL", nullable: false),
                    Reward = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Requirements = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestBoardId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.QuestID);
                    table.ForeignKey(
                        name: "FK_Quests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quests_QuestBoards_QuestBoardId",
                        column: x => x.QuestBoardId,
                        principalTable: "QuestBoards",
                        principalColumn: "QuestBoardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatesUnlocked",
                columns: table => new
                {
                    GuildMemberId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AchievementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatesUnlocked", x => new { x.GuildMemberId, x.AchievementId });
                    table.ForeignKey(
                        name: "FK_DatesUnlocked_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "AchievementID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatesUnlocked_GuildMembers_GuildMemberId",
                        column: x => x.GuildMemberId,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuildMemberTeam",
                columns: table => new
                {
                    MembersUserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Team1TeamID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMemberTeam", x => new { x.MembersUserID, x.Team1TeamID });
                    table.ForeignKey(
                        name: "FK_GuildMemberTeam_GuildMembers_MembersUserID",
                        column: x => x.MembersUserID,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildMemberTeam_Teams_Team1TeamID",
                        column: x => x.Team1TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mages",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    MP = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mages", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Mages_GuildMembers_UserID",
                        column: x => x.UserID,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mages_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID");
                });

            migrationBuilder.CreateTable(
                name: "Mounts",
                columns: table => new
                {
                    MountID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    GuildMemberId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mounts", x => x.MountID);
                    table.ForeignKey(
                        name: "FK_Mounts_GuildMembers_GuildMemberId",
                        column: x => x.GuildMemberId,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Priests",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DivinePower = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priests", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Priests_GuildMembers_UserID",
                        column: x => x.UserID,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Priests_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID");
                });

            migrationBuilder.CreateTable(
                name: "Warriors",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArmorClass = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamID = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warriors", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Warriors_GuildMembers_UserID",
                        column: x => x.UserID,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warriors_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID");
                });

            migrationBuilder.CreateTable(
                name: "DatesTaken",
                columns: table => new
                {
                    GuildMemberId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatesTaken", x => new { x.GuildMemberId, x.QuestId });
                    table.ForeignKey(
                        name: "FK_DatesTaken_GuildMembers_GuildMemberId",
                        column: x => x.GuildMemberId,
                        principalTable: "GuildMembers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatesTaken_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "QuestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatesTaken_QuestId",
                table: "DatesTaken",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_DatesUnlocked_AchievementId",
                table: "DatesUnlocked",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildMemberTeam_Team1TeamID",
                table: "GuildMemberTeam",
                column: "Team1TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Mages_TeamID",
                table: "Mages",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Mounts_GuildMemberId",
                table: "Mounts",
                column: "GuildMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Priests_TeamID",
                table: "Priests",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_CustomerId",
                table: "Quests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestBoardId_Priority",
                table: "Quests",
                columns: new[] { "QuestBoardId", "Priority" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warriors_TeamID",
                table: "Warriors",
                column: "TeamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatesTaken");

            migrationBuilder.DropTable(
                name: "DatesUnlocked");

            migrationBuilder.DropTable(
                name: "GuildMemberTeam");

            migrationBuilder.DropTable(
                name: "Mages");

            migrationBuilder.DropTable(
                name: "Mounts");

            migrationBuilder.DropTable(
                name: "Priests");

            migrationBuilder.DropTable(
                name: "Warriors");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "GuildMembers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "QuestBoards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
