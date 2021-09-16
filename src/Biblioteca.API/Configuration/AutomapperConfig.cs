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
            CreateMap<Escrito, EscritoDTO>().ReverseMap();
            CreateMap<Aluno, AlunoDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Emprestimo, EmprestimoDTO>().ReverseMap();
        }
    }
}