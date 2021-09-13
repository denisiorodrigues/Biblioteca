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
    public class AlunoService : BaseService, IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository; 
        private readonly IEnderecoRepository _enderecoRepository;

        public AlunoService(IAlunoRepository alunoRepository, IEnderecoRepository enderecoRepository, INotificador notificador)
            :base(notificador)
        {
            _alunoRepository = alunoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Aluno aluno)
        {
            if (!ExecutarValidacao(new AlunoValidation(), aluno) && !ExecutarValidacao(new EnderecoValidation(), aluno.Endereco))
            {
                return;
            }

            if (_alunoRepository.Buscar(x => x.Nome == aluno.Nome).Result.Any())
            {
                Notificar($"Já esiste um aluno cadastrado com o nome : {aluno.Nome.ToUpper()}");
                return;
            }

            await _alunoRepository.Adicionar(aluno);
        }

        public async Task Atualizar(Aluno aluno)
        {
            if (!ExecutarValidacao(new AlunoValidation(), aluno) && !ExecutarValidacao(new EnderecoValidation(), aluno.Endereco))
            {
                return;
            }

            await _alunoRepository.Atualizar(aluno);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco))
            {
                return;
            }
            
            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            var aluno = await _alunoRepository.ObterAlunoEmprestimo(id);
            if (aluno.Emprestimos.Count() > 0)
            {
                Notificar($"O aluno {aluno.Nome} possui emprestimos.");
            }
            
            await _alunoRepository.Remover(id);
        }

        public void Dispose()
        {
            _alunoRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
