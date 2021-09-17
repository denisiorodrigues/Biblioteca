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
using Biblioteca.Business.Interfaces;

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
                                INotificador notificador,
                                IMapper mapper)
        :base(notificador)
        {
            _autorRepository = autorRepository;
            _autorService = autorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutores()
        {
            return Ok(_mapper.Map<IEnumerable<AutorDTO>>( await _autorRepository.ObterTodos()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AutorDTO>> GetAutor(Guid id)
        {
            var autor = await ObterAutor(id);

            if (autor == null)
            {
                return NotFound();
            }

            return CustomResponse(autor);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAutor(Guid id, AutorDTO autorDTO)
        {
            if (id != autorDTO.Id)
            {
                return BadRequest();
            }
        
            if(!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            if (await ObterAutor(id) == null)
            {
                return NotFound();
            }

            var autor = _mapper.Map<Autor>(autorDTO);
            await _autorService.Atualizar(autor);

            return CustomResponse(autorDTO);
        }

        [HttpPost]
        public async Task<ActionResult<AutorDTO>> PostAutor(AutorDTO autorDTO)
        {
            if(!ModelState.IsValid) 
            {
                return CustomResponse(ModelState);
            }

            var autor = _mapper.Map<Autor>(autorDTO);
            await _autorService.Adicionar(autor);
            autorDTO.Id = autor.Id;

            //return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
            return CustomResponse(autorDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAutor(Guid id)
        {
            var autorDTO = await ObterAutor(id);
            if (autorDTO == null)
            {
                return NotFound();
            }

            await _autorService.Remover(id);

            return CustomResponse(autorDTO);
        }

        private async Task<AutorDTO> ObterAutor(Guid id)
        {
            return _mapper.Map<AutorDTO>(await _autorRepository.ObterAutorLivros(id));
        }
    }
}