using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BunkerGame.Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGameRoomsAndVoting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "GameRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    SessionCode = table.Column<string>(type: "TEXT", nullable: false),
                    Story = table.Column<string>(type: "TEXT", nullable: true),
                    MaxPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnersCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameRoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAlive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsWinner = table.Column<bool>(type: "INTEGER", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LeftAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsProfessionRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsGenderRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAgeRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsOrientationRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHobbyRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPhobiaRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLuggageRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAdditionalInfoRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsBodyTypeRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsHealthRevealed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPersonalityRevealed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomPlayers_GameRooms_GameRoomId",
                        column: x => x.GameRoomId,
                        principalTable: "GameRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomPlayers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VotingRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameRoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EliminatedPlayerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotingRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotingRounds_GameRooms_GameRoomId",
                        column: x => x.GameRoomId,
                        principalTable: "GameRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotingRounds_RoomPlayers_EliminatedPlayerId",
                        column: x => x.EliminatedPlayerId,
                        principalTable: "RoomPlayers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VotingRoundId = table.Column<int>(type: "INTEGER", nullable: false),
                    VoterId = table.Column<int>(type: "INTEGER", nullable: false),
                    VotedForId = table.Column<int>(type: "INTEGER", nullable: false),
                    VotedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_RoomPlayers_VotedForId",
                        column: x => x.VotedForId,
                        principalTable: "RoomPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_RoomPlayers_VoterId",
                        column: x => x.VoterId,
                        principalTable: "RoomPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_VotingRounds_VotingRoundId",
                        column: x => x.VotingRoundId,
                        principalTable: "VotingRounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayers_GameRoomId",
                table: "RoomPlayers",
                column: "GameRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayers_PlayerId",
                table: "RoomPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayers_UserId",
                table: "RoomPlayers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VotedForId",
                table: "Votes",
                column: "VotedForId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId",
                table: "Votes",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VotingRoundId",
                table: "Votes",
                column: "VotingRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_VotingRounds_EliminatedPlayerId",
                table: "VotingRounds",
                column: "EliminatedPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_VotingRounds_GameRoomId",
                table: "VotingRounds",
                column: "GameRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "VotingRounds");

            migrationBuilder.DropTable(
                name: "RoomPlayers");

            migrationBuilder.DropTable(
                name: "GameRooms");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
