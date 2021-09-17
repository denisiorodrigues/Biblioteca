using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Data.Migrations
{
    public partial class AjuestandoRelacionamentoLivroAutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escritos_Livros_AutorId",
                table: "Escritos");

            migrationBuilder.CreateIndex(
                name: "IX_Escritos_LivroId",
                table: "Escritos",
                column: "LivroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escritos_Livros_LivroId",
                table: "Escritos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escritos_Livros_LivroId",
                table: "Escritos");

            migrationBuilder.DropIndex(
                name: "IX_Escritos_LivroId",
                table: "Escritos");

            migrationBuilder.AddForeignKey(
                name: "FK_Escritos_Livros_AutorId",
                table: "Escritos",
                column: "AutorId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
