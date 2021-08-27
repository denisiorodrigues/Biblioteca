using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(200)");

            builder.HasMany(p => p.Livros)
                .WithOne(p => p.Autor)
                .HasForeignKey(p => p.AutorId);

            builder.ToTable("Autores");
        }
    }
}
