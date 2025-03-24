using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_transfertransactions_AdminId",
                table: "transfertransactions",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_transfertransactions_admins_AdminId",
                table: "transfertransactions",
                column: "AdminId",
                principalTable: "admins",
                principalColumn: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transfertransactions_admins_AdminId",
                table: "transfertransactions");

            migrationBuilder.DropIndex(
                name: "IX_transfertransactions_AdminId",
                table: "transfertransactions");
        }
    }
}
