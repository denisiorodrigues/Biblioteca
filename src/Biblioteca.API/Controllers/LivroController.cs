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
using System.IO;

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
                                INotificador notificador,
                                IMapper mapper)
        :base(notificador)
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
            var livrosDTO = await ObterLivroAutor(id);

            if(livrosDTO == null) NotificarErro("Livro não encontrado");
            
            return CustomResponse(livrosDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LivroDTO>> Cadastrar(LivroDTO livroDTO)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var imgNome = Guid.NewGuid() + "_" + livroDTO.Imagem;

            if (!UploadArquivo(livroDTO.ImagemUpload, imgNome))
            {
                return CustomResponse();
            }

            livroDTO.Imagem = imgNome;
            var livro = _mapper.Map<Livro>(livroDTO);
            await _livroService.Adicionar(livro);

            return CustomResponse(livroDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Atualizar(Guid id, LivroDTO livroDTO)
        {
            if(id != livroDTO.Id) return BadRequest();

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var livro = _mapper.Map<Livro>(livroDTO);
            await _livroService.Atualizar(livro);

            return CustomResponse(livroDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Remover(Guid id)
        {
            var livro = _livroRepository.ObterPorId(id);

            if(livro == null) return NotFound();

            await _livroRepository.Remover(id);

            return CustomResponse(livro);
        }

        private bool UploadArquivo(string arquivo, string nome)
        {
            if(string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneceça uma imagem para este produto!");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nome);

            if(System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            var imagemDataByteArray  = Convert.FromBase64String(arquivo);
            System.IO.File.WriteAllBytes(filePath, imagemDataByteArray);
            
            return true;
        }

        private async Task<IEnumerable<LivroDTO>> ObterLivrosAutores(Guid id)
        {
            return _mapper.Map<IEnumerable<LivroDTO>>(await _livroRepository.ObterLivrosAutores(id));
        }
    }
}
