using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminroles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminroles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tokenblacklist",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tokenblacklist", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK__admins__RoleId__3F466844",
                        column: x => x.RoleId,
                        principalTable: "adminroles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    AccountLocked = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    PasswordResetToken = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PasswordResetExpiry = table.Column<DateTime>(type: "datetime", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_users_adminroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "adminroles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "adminprofile",
                columns: table => new
                {
                    AdminProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminprofile", x => x.AdminProfileId);
                    table.ForeignKey(
                        name: "FK__adminprof__Admin__4222D4EF",
                        column: x => x.AdminId,
                        principalTable: "admins",
                        principalColumn: "AdminId");
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(15,2)", nullable: true, defaultValue: 0.00m),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_accounts_users",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "otp",
                columns: table => new
                {
                    OTPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OTPCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otp", x => x.OTPId);
                    table.ForeignKey(
                        name: "FK__otp__UserId__52593CB8",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RequestType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK__requests__UserId__5629CD9C",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "userprofile",
                columns: table => new
                {
                    UserProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userprofile", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK__userprofi__UserI__4F7CD00D",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserToke__1788CC4C5A0172AC", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__UserToken__UserI__5AEE82B9",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    TransactionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK__transacti__Accou__60A75C0F",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK__transacti__Admin__5EBF139D",
                        column: x => x.AdminId,
                        principalTable: "admins",
                        principalColumn: "AdminId");
                    table.ForeignKey(
                        name: "FK__transacti__UserI__5FB337D6",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "transfertransactions",
                columns: table => new
                {
                    TransferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAccountId = table.Column<int>(type: "int", nullable: true),
                    ToAccountId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    TransferDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfertransactions", x => x.TransferId);
                    table.ForeignKey(
                        name: "FK__transfert__FromA__6477ECF3",
                        column: x => x.FromAccountId,
                        principalTable: "accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK__transfert__ToAcc__656C112C",
                        column: x => x.ToAccountId,
                        principalTable: "accounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_UserId",
                table: "accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_AccountNumber",
                table: "accounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_adminprofile_AdminId",
                table: "adminprofile",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "UQ_RoleName",
                table: "adminroles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_admins_RoleId",
                table: "admins",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ_Username",
                table: "admins",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_otp_UserId",
                table: "otp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_UserId",
                table: "requests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_AccountId",
                table: "transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_AdminId",
                table: "transactions",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_UserId",
                table: "transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_transfertransactions_FromAccountId",
                table: "transfertransactions",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transfertransactions_ToAccountId",
                table: "transfertransactions",
                column: "ToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_userprofile_UserId",
                table: "userprofile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ_Username_New",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adminprofile");

            migrationBuilder.DropTable(
                name: "otp");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "tokenblacklist");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "transfertransactions");

            migrationBuilder.DropTable(
                name: "userprofile");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "adminroles");
        }
    }
}
