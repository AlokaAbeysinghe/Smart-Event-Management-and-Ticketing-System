using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Event_Management_and_Ticketing_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialProjectSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    EVENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EVENTNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CATEGORY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EVENTDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    VENUE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRICE = table.Column<decimal>(type: "DECIMAL(8,2)", precision: 8, scale: 2, nullable: false),
                    AVAILABILITY = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.EVENT_ID);
                });

            migrationBuilder.CreateTable(
                name: "MEMBER",
                columns: table => new
                {
                    MEMBER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FULLNAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBER", x => x.MEMBER_ID);
                });

            migrationBuilder.CreateTable(
                name: "BOOKING",
                columns: table => new
                {
                    BOOKING_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SEATTYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QUANTITY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MEMBER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EVENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKING", x => x.BOOKING_ID);
                    table.ForeignKey(
                        name: "FK_BOOKING_EVENT_EVENT_ID",
                        column: x => x.EVENT_ID,
                        principalTable: "EVENT",
                        principalColumn: "EVENT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BOOKING_MEMBER_MEMBER_ID",
                        column: x => x.MEMBER_ID,
                        principalTable: "MEMBER",
                        principalColumn: "MEMBER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REVIEW",
                columns: table => new
                {
                    REVIEW_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    COMMENTS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MEMBER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EVENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW", x => x.REVIEW_ID);
                    table.ForeignKey(
                        name: "FK_REVIEW_EVENT_EVENT_ID",
                        column: x => x.EVENT_ID,
                        principalTable: "EVENT",
                        principalColumn: "EVENT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REVIEW_MEMBER_MEMBER_ID",
                        column: x => x.MEMBER_ID,
                        principalTable: "MEMBER",
                        principalColumn: "MEMBER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BOOKING_EVENT_ID",
                table: "BOOKING",
                column: "EVENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKING_MEMBER_ID",
                table: "BOOKING",
                column: "MEMBER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_EVENT_ID",
                table: "REVIEW",
                column: "EVENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEW_MEMBER_ID",
                table: "REVIEW",
                column: "MEMBER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOOKING");

            migrationBuilder.DropTable(
                name: "REVIEW");

            migrationBuilder.DropTable(
                name: "EVENT");

            migrationBuilder.DropTable(
                name: "MEMBER");
        }
    }
}
