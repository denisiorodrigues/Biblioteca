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
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(BibliotecaContext context) :
            base(context)
        {

        }

        public async Task<IList<Aluno>> ObterAlunoEmprestimo(Guid id)
        {
            return await Context
                        .Alunos
                        .Include(x => x.Emprestimos)
                        .Where(x => x.Id == id)
                        .ToListAsync();
        }

        public async Task<IList<Aluno>> ObterAlunoEndereco(Guid id)
        {
            return await Context
                         .Alunos
                         .Include(x => x.Endereco)
                         .Where(x => x.Id == id)
                         .ToListAsync();
        }

        public async Task<IList<Aluno>> ObterAlunoEmprestimoEndereco(Guid id)
        {
            return await Context
                         .Alunos
                         .Include(x => x.Emprestimos)
                         .Include(x => x.Endereco)
                         .Where(x => x.Id == id)
                         .ToListAsync();
        }
    }
}
