using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Data.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(BibliotecaContext context): base(context)
        {

        }

        public async Task<Autor> ObterAutorLivros(Guid id)
        {
            return await Context.Autores
                        .AsNoTracking()
                        .Include(x => x.Livros)
                        .FirstOrDefaultAsync(x => x.Id == id );
        }

        public async Task<Autor> ObterPorNome(string nome)
        {
            return await Context.Autores
                        .AsNoTracking()
                        .Include(x => x.Livros)
                        .FirstOrDefaultAsync(x => x.Nome.Contains(nome));
        }
    }
}
