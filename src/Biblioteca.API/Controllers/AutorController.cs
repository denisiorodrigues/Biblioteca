using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Business.Models;
using Biblioteca.Business.Interfaces.Repository;
using AutoMapper;
using Biblioteca.API.DTO;
using Biblioteca.Business.Interfaces.Services;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : MainController
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;

        public AutorController(IAutorRepository autorRepository,
                                IAutorService autorService,
                                IMapper mapper)
        {
            _autorRepository = autorRepository;
            _autorService = autorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutores()
        {
            return Ok(_mapper.Map<AutorDTO>( await _autorRepository.ObterTodos()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AutorDTO>> GetAutor(Guid id)
        {
            var autor = await ObterAutor(id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(Guid id, AutorDTO autorDTO)
        {
            if (id != autorDTO.Id)
            {
                return BadRequest();
            }
        
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var autor = _mapper.Map<Autor>(autorDTO);
            await _autorService.Atualizar(autor);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(Autor autor)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            await _autorService.Adicionar(autor);

            return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutor(Guid id)
        {
            var autor = await ObterAutor(id);
            if (autor == null)
            {
                return NotFound();
            }

            if(autor.Livros.Any())
            {
                //Notificar("O autor possui livro cadastrado");
                return BadRequest();
            }

            await _autorService.Remover(id);

            return Ok(autor);
        }

        private async Task<AutorDTO> ObterAutor(Guid id)
        {
            return _mapper.Map<AutorDTO>(await _autorRepository.ObterAutorLivros(id));
        }
    }
}