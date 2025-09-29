using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contas_Documento",
                table: "Contas",
                column: "Documento",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contas_Documento",
                table: "Contas");
        }
    }
}
