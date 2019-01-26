using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMyAccount.Migrations
{
    public partial class MySecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecordDetails",
                columns: table => new
                {
                    RecordDetailId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Asset = table.Column<decimal>(nullable: false),
                    Debt = table.Column<decimal>(nullable: false),
                    Info = table.Column<string>(nullable: true),
                    RecordId = table.Column<long>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordDetails", x => x.RecordDetailId);
                    table.ForeignKey(
                        name: "FK_RecordDetails_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordDate = table.Column<DateTime>(nullable: false),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecordDetails_AccountId",
                table: "RecordDetails",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecordDetails");

            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
