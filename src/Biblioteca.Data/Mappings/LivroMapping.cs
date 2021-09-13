using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Titulo).IsRequired().HasColumnType("varchar(200)");
            builder.Property(p => p.Resumo).IsRequired().HasColumnType("varchar(500)");
            builder.Property(p => p.Exemplares).IsRequired();
            builder.Property(p => p.Publicado).IsRequired();
            builder.Property(p => p.Imagem).HasColumnType("varchar(255)");
            builder.Property(p => p.Situacao).IsRequired();

            builder.HasMany(p => p.Emprestimos)
                .WithOne(p => p.Livro)
                .HasForeignKey(p => p.LivroId);


            //TODO: Delete cascade habilitado
            builder.HasMany(p => p.Autores)
                .WithOne(p => p.Livro)
                .HasForeignKey(p => p.AutorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Livros");
        }
    }
}
