using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorseBets.Migrations
{
    /// <inheritdoc />
    public partial class BetUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Bets",
                newName: "CreationTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Bets",
                newName: "CreateTime");
        }
    }
}
