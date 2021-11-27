using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHotel.DAL.Migrations
{
    public partial class AddStartPriceCoolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PriceAtTheMomentStart",
                table: "CategoryInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAtTheMomentStart",
                table: "CategoryInfos");
        }
    }
}
