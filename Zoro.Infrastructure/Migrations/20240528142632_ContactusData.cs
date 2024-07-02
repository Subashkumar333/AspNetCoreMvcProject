using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContactusData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactusData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YourName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YourEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YourPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YourMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactusData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactusData");
        }
    }
}
