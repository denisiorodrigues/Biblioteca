using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data.Mappings
{
    class EscritoMapping : IEntityTypeConfiguration<Escrito>
    {
        public void Configure(EntityTypeBuilder<Escrito> builder)
        {
            builder.HasKey( k => new { k.AutorId, k.LivroId });

            // builder.HasOne(p => p.Autor)
            //     .WithMany(p => p.Livros)
            //     .HasForeignKey(f => f.AutorId);

            // builder.HasOne(p => p.Livro)
            //     .WithMany(p => p.Autores)
            //     .HasForeignKey(f => f.LivroId);

            builder.ToTable("Escritos");
        }
    }
}
