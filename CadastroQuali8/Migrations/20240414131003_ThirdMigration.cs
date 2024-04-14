using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QualiCadastro.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cadastros");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_IdCadastro",
                table: "Emails",
                column: "IdCadastro");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Cadastros_IdCadastro",
                table: "Emails",
                column: "IdCadastro",
                principalTable: "Cadastros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Cadastros_IdCadastro",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Emails_IdCadastro",
                table: "Emails");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Emails",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cadastros",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
