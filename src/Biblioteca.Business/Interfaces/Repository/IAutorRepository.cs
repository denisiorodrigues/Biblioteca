using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Repository
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<Autor> ObterAutorLivros(Guid id);

        Task<Autor> ObterPorNome(string nome);
    }
}
