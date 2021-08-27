using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biblioteca.Data.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public DbSet<Escrito> Escritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaContext).Assembly);

            //Deletando delete on cascate
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //
            //optionsBuilder.UseSqlServer("Server=localhost;Database=Biblioteca;User Id=SA;Password=*UHB*UHB");

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
