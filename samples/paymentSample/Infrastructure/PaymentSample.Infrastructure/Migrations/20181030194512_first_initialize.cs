using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentSample.Infrastructure.Migrations
{
    public partial class first_initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payment");

            migrationBuilder.CreateTable(
                name: "PaymenTransactionStatus",
                schema: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymenTransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactionType",
                schema: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                schema: "payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true),
                    CurrencyCode = table.Column<int>(nullable: true),
                    TransactionDateTime = table.Column<DateTime>(nullable: false),
                    MerchantTransactionDateTime = table.Column<DateTime>(nullable: true),
                    Msisdn = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    ResponseCode = table.Column<string>(nullable: true),
                    ResponseMessage = table.Column<string>(nullable: true),
                    TransactionStatusId = table.Column<int>(nullable: false),
                    PaymenTransactionStatusId = table.Column<int>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    ReferanceTransactionId = table.Column<int>(nullable: true),
                    ReferanceTransactionId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_PaymenTransactionStatus_PaymenTransactionStatusId",
                        column: x => x.PaymenTransactionStatusId,
                        principalSchema: "payment",
                        principalTable: "PaymenTransactionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_PaymentTransactions_ReferanceTransactionId1",
                        column: x => x.ReferanceTransactionId1,
                        principalSchema: "payment",
                        principalTable: "PaymentTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_PaymentTransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalSchema: "payment",
                        principalTable: "PaymentTransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymenTransactionStatusId",
                schema: "payment",
                table: "PaymentTransactions",
                column: "PaymenTransactionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_ReferanceTransactionId1",
                schema: "payment",
                table: "PaymentTransactions",
                column: "ReferanceTransactionId1");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TransactionTypeId",
                schema: "payment",
                table: "PaymentTransactions",
                column: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "PaymenTransactionStatus",
                schema: "payment");

            migrationBuilder.DropTable(
                name: "PaymentTransactionType",
                schema: "payment");
        }
    }
}
