using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Business.Services
{
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        public AutorService(IAutorRepository autorRepository, INotificador notificador):
            base(notificador)
        {
            _autorRepository = autorRepository;
        }

        public async Task Adicionar(Autor aluno)
        {
            await _autorRepository.Adicionar(aluno);
        }

        public async Task Atualizar(Autor aluno)
        {
            await _autorRepository.Atualizar(aluno);
        }

        public async Task Remover(Guid id)
        {
            var autor = await _autorRepository.ObterAutorLivros(id);
            if (autor.Livros.Any())
            {
                Notificar($"Autor {autor.Nome.ToUpper()} possui livro cadastrado, não é possível excluir.");
                return;
            }
            
            await _autorRepository.Remover(id);
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }
    }
}
