using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HorseBetsServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Horses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Speed = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    HorseId = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bets_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorseCoefficients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Coefficient = table.Column<float>(type: "real", nullable: false),
                    HorseId = table.Column<string>(type: "text", nullable: true),
                    MatchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorseCoefficients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorseCoefficients_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HorseCoefficients_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorseMatch",
                columns: table => new
                {
                    MatchesId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorseMatch", x => new { x.MatchesId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_HorseMatch_Horses_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Horses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorseMatch_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorseMatchWinner",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    HorseId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorseMatchWinner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorseMatchWinner_Horses_HorseId",
                        column: x => x.HorseId,
                        principalTable: "Horses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorseMatchWinner_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_ClientId",
                table: "Bets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_HorseId",
                table: "Bets",
                column: "HorseId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_MatchId",
                table: "Bets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseCoefficients_HorseId",
                table: "HorseCoefficients",
                column: "HorseId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseCoefficients_MatchId",
                table: "HorseCoefficients",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseMatch_ParticipantsId",
                table: "HorseMatch",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseMatchWinner_HorseId",
                table: "HorseMatchWinner",
                column: "HorseId");

            migrationBuilder.CreateIndex(
                name: "IX_HorseMatchWinner_MatchId",
                table: "HorseMatchWinner",
                column: "MatchId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "HorseCoefficients");

            migrationBuilder.DropTable(
                name: "HorseMatch");

            migrationBuilder.DropTable(
                name: "HorseMatchWinner");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Horses");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
