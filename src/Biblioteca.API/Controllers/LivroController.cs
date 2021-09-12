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

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : MainController
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;

        public LivroController(ILivroRepository livroRepository,
                                ILivroService livroService,
                                IMapper mapper)
        {
            _livroRepository = livroRepository;
            _livroService = livroService;
            _mapper = mapper;
        }

        // GET: api/Livro
        [HttpGet]
        public async Task<IEnumerable<LivroDTO>> GetLivros()
        {
            var livros = _mapper.Map<IEnumerable<LivroDTO>>(await _livroRepository.ObterTodos());
            return livros;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<LivroDTO>>> ObterLivroAutor(Guid id)
        {
            var livros =  await ObterLivroAutor(id);

            if(livros == null) BadRequest();
            
            return Ok(livros);
        }

        [HttpPost]
        public async Task<ActionResult<LivroDTO>> Cadastrar(LivroDTO livroDTO)
        {
            if(!ModelState.IsValid) return BadRequest();

            var livro = _mapper.Map<Livro>(livroDTO);
            await _livroService.Adicionar(livro);

            return Ok(livroDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Atualizar(Guid id, LivroDTO livroDTO)
        {
            if(id != livroDTO.Id) return BadRequest(0);

            if(!ModelState.IsValid) return BadRequest();

            var livro = _mapper.Map<Livro>(livroDTO);

            await _livroService.Atualizar(livro);

            return Ok(livroDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Remover(Guid id)
        {
            var livro = _livroRepository.ObterPorId(id);

            if(livro == null) return NotFound();

            await _livroRepository.Remover(id);

            return Ok(livro);
        }

        private async Task<IEnumerable<LivroDTO>> ObterLivrosAutores(Guid id)
        {
            return _mapper.Map<IEnumerable<LivroDTO>>(await _livroRepository.ObterLivrosAutores(id));
        }
    }
}
