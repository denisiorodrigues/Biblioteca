using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Repository
{
    public interface IEmprestimoRepository : IRepository<Emprestimo>
    {
        Task<IList<Emprestimo>> ObterPendenteDeDevolucao();
        Task<IList<Emprestimo>> ObterEmprestimoPorAluno(Guid id);
        Task<IList<Emprestimo>> ObterEmprestimoPorLivro(Guid id);
        Task<IList<Emprestimo>> ObterEmprestimoPedentePorAluno(Guid id);
        Task<IList<Emprestimo>> ObterEmprestimoPedentePorLivro(Guid id);
    }
}
