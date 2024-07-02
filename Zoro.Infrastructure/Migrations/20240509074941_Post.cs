﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BycycleCategories = table.Column<int>(type: "int", nullable: false),
                    BycycleTypes = table.Column<int>(type: "int", nullable: false),
                    PriceFrom = table.Column<double>(type: "float", nullable: false),
                    PriceTo = table.Column<double>(type: "float", nullable: false),
                    Ratings = table.Column<int>(type: "int", nullable: false),
                    VehicleImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_VechicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VechicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_BrandId",
                table: "Post",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_VehicleTypeId",
                table: "Post",
                column: "VehicleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
