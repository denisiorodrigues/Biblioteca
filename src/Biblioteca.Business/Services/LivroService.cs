using Biblioteca.Business.Interfaces;
using Biblioteca.Business.Interfaces.Repository;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Models;
using Biblioteca.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Business.Services
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IEscritoRepository _escritoRepository;

        public LivroService(ILivroRepository livroRepository, 
                            IEscritoRepository escritoRepository,
                            INotificador notificador)
            : base(notificador)
        {
            _livroRepository = livroRepository;
            _escritoRepository = escritoRepository;
        }

        public async Task Adicionar(Livro livro)
        {
            if(!ExecutarValidacao(new LivroValidation(), livro))
            {
                return;
            }
                
            await _livroRepository.Adicionar(livro);
        }

        public async Task Atualizar(Livro livro)
        {
            if(!ExecutarValidacao(new LivroValidation(), livro))
            {
                return;
            }
            
            if(livro.Autores.Any())
            {
                _escritoRepository.RemoverPorLivro(livro.Id);
            }

            await _livroRepository.Atualizar(livro);
        }

        public async Task Remover(Guid id)
        {
            if (await _livroRepository.TemEmprestimo(id))
            {
                Notificar("O livro não pode ser excluído por contem emprestimo ativo.");
            }

            await _livroRepository.Remover(id);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }
    }
}
