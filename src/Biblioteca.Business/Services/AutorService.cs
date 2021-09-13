using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Models;
using Biblioteca.Business.Validations;
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

        public async Task Adicionar(Autor autor)
        {
            if (await IsExiste(autor.Nome))
            {
                Notificar($"Já existe um autor cadastrado com o nome {autor.Nome.ToUpper()}");
                return;
            }
            
            if(ExecutarValidacao(new AutorValidation(), autor))
            {
                return ;
            }
            
            await _autorRepository.Adicionar(autor);
        }

        public async Task Atualizar(Autor autor)
        {
            if (await IsExiste(autor.Nome))
            {
                Notificar($"Já existe um autor cadastrado com o nome {autor.Nome.ToUpper()}");
                return;
            }

            if(ExecutarValidacao(new AutorValidation(), autor))
            {
                return ;
            }

            await _autorRepository.Atualizar(autor);
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

        public async Task<bool> IsExiste(string nome)
        {
            return await _autorRepository.IsExiste(nome);
        }

        public void Dispose()
        {
            _autorRepository?.Dispose();
        }
    }
}
