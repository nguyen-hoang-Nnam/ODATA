using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ass2.Model.Migrations
{
    /// <inheritdoc />
    public partial class update_footballPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballClub",
                columns: table => new
                {
                    FootballClubID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClubShortDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    SoccerPracticeField = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Mascos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Football__9127950498A46AB5", x => x.FootballClubID);
                });

            migrationBuilder.CreateTable(
                name: "PremierLeagueAccount",
                columns: table => new
                {
                    AccID = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PremierL__91CBC3988138D158", x => x.AccID);
                });

            migrationBuilder.CreateTable(
                name: "FootballPlayer",
                columns: table => new
                {
                    FootballPlayerID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Achievements = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlayerExperiences = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Nomination = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    FootballClubID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Football__6D5466C3E2FE101F", x => x.FootballPlayerID);
                    table.ForeignKey(
                        name: "FK__FootballP__Footb__3C69FB99",
                        column: x => x.FootballClubID,
                        principalTable: "FootballClub",
                        principalColumn: "FootballClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FootballPlayer_FootballClubID",
                table: "FootballPlayer",
                column: "FootballClubID");

            migrationBuilder.CreateIndex(
                name: "UQ__PremierL__49A14740704BF9BC",
                table: "PremierLeagueAccount",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballPlayer");

            migrationBuilder.DropTable(
                name: "PremierLeagueAccount");

            migrationBuilder.DropTable(
                name: "FootballClub");
        }
    }
}
