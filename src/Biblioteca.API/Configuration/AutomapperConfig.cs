using AutoMapper;
using Biblioteca.API.DTO;
using Biblioteca.Business.Models;

namespace Biblioteca.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Livro, LivroDTO>().ReverseMap();
            CreateMap<Autor, AutorDTO>().ReverseMap();
        }
    }
}