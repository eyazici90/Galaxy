using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerSample.Infrastructure.Migrations
{
    public partial class concurrencyStamp_adds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "customer",
                table: "Merchants");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "customer",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "customer",
                table: "Brands");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "customer",
                table: "Merchants",
                rowVersion: true,
                nullable: true);
        }
    }
}
