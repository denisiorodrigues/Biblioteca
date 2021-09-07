using Biblioteca.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mappings
{
    public class EmprestimoMapping : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Data).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.Devolucao).IsRequired();
            builder.Property(p => p.Devolvido);

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
