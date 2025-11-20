using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lucas_tickets.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Paymentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowID = table.Column<int>(type: "int", nullable: false),
                    ShowsShowId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Paymentid);
                    table.ForeignKey(
                        name: "FK_Payment_Shows_ShowsShowId",
                        column: x => x.ShowsShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ShowsShowId",
                table: "Payment",
                column: "ShowsShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
