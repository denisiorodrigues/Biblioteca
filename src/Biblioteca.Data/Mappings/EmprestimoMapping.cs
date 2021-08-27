using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data.Mappings
{
    public class EmprestimoMapping : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Data).IsRequired();
            builder.Property(p => p.Devolucao).IsRequired();

            builder.HasOne(p => p.Aluno)
                .WithMany(p => p.Emprestimos)
                .HasForeignKey(p => p.AlunoId);

            builder.HasOne(p => p.Livro)
                .WithMany(p => p.Emprestimos)
                .HasForeignKey(p => p.LivroId);

            builder.ToTable("Emprestimos");
        }
    }
}
