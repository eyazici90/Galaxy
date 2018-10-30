using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerSample.Infrastructure.Migrations
{
    public partial class merhcant_rowversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "customer",
                table: "Merchants",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "customer",
                table: "Merchants");
        }
    }
}
