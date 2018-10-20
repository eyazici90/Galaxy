using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerSample.Infrastructure.Migrations
{
    public partial class first_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EMail = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Gsm = table.Column<string>(nullable: true),
                    SNCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LimitTypes",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Limits",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LimitTypeId = table.Column<int>(nullable: false),
                    LimitValue = table.Column<decimal>(nullable: false),
                    LimitName = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Limits_LimitTypes_LimitTypeId",
                        column: x => x.LimitTypeId,
                        principalSchema: "customer",
                        principalTable: "LimitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                schema: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    Gsm = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Vkn = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    IsFraud = table.Column<bool>(nullable: false),
                    LimitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchants_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "customer",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Merchants_Limits_LimitId",
                        column: x => x.LimitId,
                        principalSchema: "customer",
                        principalTable: "Limits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Limits_LimitTypeId",
                schema: "customer",
                table: "Limits",
                column: "LimitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_BrandId",
                schema: "customer",
                table: "Merchants",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_LimitId",
                schema: "customer",
                table: "Merchants",
                column: "LimitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Groups",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Merchants",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "Limits",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "LimitTypes",
                schema: "customer");
        }
    }
}
