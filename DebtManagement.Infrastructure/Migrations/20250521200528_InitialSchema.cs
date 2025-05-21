using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebtManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identificador único da dívida")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebtNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Número único do título"),
                    DebtorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebtorCPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FineRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                },
                comment: "Tabela de dívidas");

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    DebtId = table.Column<int>(type: "int", nullable: false),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => new { x.DebtId, x.InstallmentNumber });
                    table.ForeignKey(
                        name: "FK_Installments_Debts_DebtId",
                        column: x => x.DebtId,
                        principalTable: "Debts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Debts");
        }
    }
}
