using ApiLibros.Models;
using ApiLibros.Models.Dto;
using AutoMapper;

namespace ApiLibros.LibroMapper
{
    public class LibroMappers : Profile
    {
        public LibroMappers()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDtoCreate>().ReverseMap();

            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<Autor, AutorCreateDto>().ReverseMap();
            CreateMap<Autor, AutorUpdateDto>().ReverseMap();

            CreateMap<Libro, LibroDto>().ReverseMap();
            CreateMap<Libro, LibroCreateDto>().ReverseMap();
            CreateMap<Libro, LibroUpdateDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();



        }


    }
}
