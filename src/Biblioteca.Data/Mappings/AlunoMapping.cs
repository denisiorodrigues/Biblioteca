using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data.Mappings
{
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(200)");

            //Mapeamento de 1:N
            builder.HasMany(p => p.Emprestimos)
                .WithOne(p => p.Aluno)
                .HasForeignKey(f => f.AlunoId);

            //Mapeamento de 1:1
            builder.HasOne(p => p.Endereco)
                .WithOne(p => p.Aluno)
                .HasForeignKey<Aluno>(f => f.EnderecoId);

            builder.ToTable("Alunos");
        }
    }
}
