using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Data.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(BibliotecaContext context): base(context)
        {

        }

        public async Task<bool> IsExiste(string nome) => await Context.Autores
                        .AsNoTracking()
                        .Where(x => !string.IsNullOrEmpty(nome) && x.Nome.Contains(nome.TrimEnd().TrimStart()))
                        .AnyAsync();

        public async Task<Autor> ObterAutorLivros(Guid id) => await Context.Autores
                        .AsNoTracking()
                        .Include(x => x.Livros)
                        .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Autor> ObterPorNome(string nome) => await Context.Autores
                        .AsNoTracking()
                        .Include(x => x.Livros)
                        .FirstOrDefaultAsync(x => x.Nome.ToLower().Contains(nome.ToLower().TrimEnd().TrimStart()));
    }
}
