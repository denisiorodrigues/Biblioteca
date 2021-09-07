using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca.Data.Migrations
{
    public partial class DataDevolucaoEmprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Emprestimos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "Devolvido",
                table: "Emprestimos",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Devolvido",
                table: "Emprestimos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Emprestimos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
