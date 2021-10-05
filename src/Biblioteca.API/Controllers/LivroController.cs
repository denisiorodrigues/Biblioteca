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
using Microsoft.AspNetCore.Authorization;

namespace Biblioteca.API.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<LivroDTO>> GetLivros()
        {
            var livros = _mapper.Map<IEnumerable<LivroDTO>>(await _livroRepository.ObterTodos());
            return livros;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> ObterLivroAutor(Guid id)
        {
            var livrosDTO = await ObterLivroAutorEmprestimo(id); 

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

        
        [HttpPost("adicionar")]
        public async Task<ActionResult<LivroDTO>> CadastrarAlternativo(LivroImagemDTO livroDTO)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemPrefixo = Guid.NewGuid() + "_";

            if (!await UploadArquivoAlternativo(livroDTO.ImagemUpload, imagemPrefixo))
            {
                return CustomResponse();
            }

            livroDTO.Imagem = imagemPrefixo + livroDTO.ImagemUpload.Name;
            var livro = _mapper.Map<Livro>(livroDTO);
            await _livroService.Adicionar(livro);

            return CustomResponse(livroDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LivroDTO>> Atualizar(Guid id, LivroDTO livroDTO)
        {
            if(id != livroDTO.Id) return BadRequest();

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            // var livro = _mapper.Map<Livro>(livroDTO);
            var livro = await _livroRepository.ObterPorId(id);
            if(livro == null)
            {
                NotificarErro("Livro não encontrado.");
                return CustomResponse();
            }

            if(livroDTO.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_" + livroDTO.Imagem;
                if(!UploadArquivo(livroDTO.ImagemUpload, imgPrefixo))
                {
                    return CustomResponse();
                }
            }

            livro.Imagem = livroDTO.Imagem;
            livro.Publicado = livroDTO.Publicado;
            livro.Resumo = livroDTO.Resumo;
            livro.Situacao = (SituacaoLivroEnum)livroDTO.Situacao;
            livro.Titulo = livroDTO.Titulo;

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

        [RequestSizeLimit(40000000)]
        //[DisableRequestSizeLimit]
        [HttpPost("imagem")]
        public ActionResult UploadImagem(IFormFile file)
        {
            return Ok(file);
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

        private async Task<bool> UploadArquivoAlternativo(IFormFile file, string nomePrefixo)
        {
            if(file == null || file.Length == 0)
            {
                NotificarErro("Forneceça uma imagem para este produto!");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomePrefixo + file.Name);

            if(System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        private async Task<IEnumerable<LivroDTO>> ObterLivrosAutores(Guid id)
        {
            return _mapper.Map<IEnumerable<LivroDTO>>(await _livroRepository.ObterLivrosAutores(id));
        }

        private async Task<LivroDTO> ObterLivroAutorEmprestimo(Guid id)
        {
            return _mapper.Map<LivroDTO>(await _livroRepository.ObterLivroAutorEmprestimo(id));
        }
    }
}
