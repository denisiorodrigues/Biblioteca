using Biblioteca.Business.Models;
using System;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces.Services
{
    public interface IAutorService
    {
        Task Adicionar(Autor autor);
    
        Task Atualizar(Autor autor);
    
        Task Remover(Guid id);

        Task<bool> IsExiste(string nome);

        void Dispose();
    }
}
