using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Business.Models;
using Biblioteca.Data.Context;
using Biblioteca.Business.Interfaces.Repository;
using AutoMapper;
using Biblioteca.API.DTO;
using Biblioteca.Business.Interfaces.Services;
using Biblioteca.Business.Interfaces;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : MainController
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IAlunoService _alunoService;
        private readonly IMapper _mapper;

        public AlunoController(IAlunoRepository alunoRepository,
                                IAlunoService alunoService,
                                INotificador notificador,
                                IMapper mapper,
                                IUser user) :base(notificador, user)
        {
            _alunoRepository = alunoRepository;
            _alunoService = alunoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AlunoDTO>> GetAlunos()
        {
            return _mapper.Map<IEnumerable<AlunoDTO>>(await _alunoRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> ObterAluno(Guid id)
        {
            var alunoDTO = await ObterAlunoEmprestimoEndereco(id);

            if(alunoDTO == null) NotificarErro("Aluno não encontrado");
            
            return CustomResponse(alunoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LivroDTO>> Cadastrar(AlunoDTO alunoDTO)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var aluno = _mapper.Map<Aluno>(alunoDTO);
            await _alunoService.Adicionar(aluno);

            return CustomResponse(alunoDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Atualizar(Guid id, AlunoDTO alunoDTO)
        {
            if(id != alunoDTO.Id) return BadRequest();

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var aluno = _mapper.Map<Aluno>(alunoDTO);
            await _alunoService.Atualizar(aluno);

            return CustomResponse(alunoDTO);
        }

        [HttpPut("endereco/{id:guid}")]
        public async Task<ActionResult> AtualizarEndereco(Guid id, EnderecoDTO enderecoDTO)
        {
            if(id != enderecoDTO.Id) return BadRequest();
            
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _alunoService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoDTO));

            return CustomResponse(enderecoDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Remover(Guid id)
        {
            var livro = _alunoRepository.ObterPorId(id);

            if(livro == null) return NotFound();

            await _alunoRepository.Remover(id);

            return CustomResponse(livro);
        }

        private async Task<AlunoDTO> ObterAlunoEmprestimoEndereco(Guid id)
        {
            var entityResult = await _alunoRepository.ObterAlunoEmprestimoEndereco(id);
            return _mapper.Map<AlunoDTO>(entityResult);
        }
    }
}
