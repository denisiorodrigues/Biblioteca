using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(BibliotecaContext context)
            :base(context)
        {

        }

        public async Task<Endereco> ObterEnderecoPorAluno(Guid id)
        {
            return await Context.Enderecos
                .AsNoTracking()
                .Include(c => c.Aluno)
                .FirstOrDefaultAsync(x => x.AlunoId == id);
        }
    }
}
