using ApiLibros.Models;
using System.Collections.Generic;

namespace ApiLibros.Repository.Irepsitory
{
    public interface ILibroRepository
    {
        ICollection<Libro> GetLibros();
        ICollection<Libro> GetLibrosEnCategoria(int categoriaId);
        //ICollection<Libro> GetLibrosEnAutor(int autorId);

        Libro GetLibro(int libroID);
        bool ExisteLibro(string nombre);
        IEnumerable<Libro> BuscarLibros(string nombre);
        bool ExisteLibro(int id);
        bool CrearLibro(Libro libro);
        bool ActualizarLibro(Libro libro);
        bool BorrarLibro(Libro libro);
        bool Guardar();




    }
}
