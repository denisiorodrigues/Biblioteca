using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Repository
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<IList<Aluno>> ObterAlunoEmprestimo(Guid id);

        Task<IList<Aluno>> ObterAlunoEndereco(Guid id);

        Task<IList<Aluno>> ObterAlunoEmprestimoEndereco(Guid id);
    }
}
