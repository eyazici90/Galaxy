using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerSample.Infrastructure.Migrations
{
    public partial class brand_to_fullyaudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                schema: "customer",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                schema: "customer",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                schema: "customer",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifierUserId",
                schema: "customer",
                table: "Brands",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "customer",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                schema: "customer",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                schema: "customer",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                schema: "customer",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                schema: "customer",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "customer",
                table: "Brands");
        }
    }
}
