using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class ininini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "transfertransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_transfertransactions_UserId",
                table: "transfertransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_transfertransactions_users_UserId",
                table: "transfertransactions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transfertransactions_users_UserId",
                table: "transfertransactions");

            migrationBuilder.DropIndex(
                name: "IX_transfertransactions_UserId",
                table: "transfertransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "transfertransactions");
        }
    }
}
