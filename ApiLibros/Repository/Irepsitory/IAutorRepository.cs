using ApiLibros.Models;
using System.Collections.Generic;

namespace ApiLibros.Repository.Irepsitory
{
    public interface IAutorRepository
    {
        ICollection<Autor> GetAutors();
        Autor GetAutor(int autorID);
        bool ExisteAutor(int autorID);
        IEnumerable<Autor> BuscarAutor(string nombre);
        bool ExisteAutor(string nombre);
        bool CreateAutor(Autor autor);
        bool ActualizarAutor(Autor autor);
        bool BorrarAutor(Autor autor);
        bool Guardar();
    }
}
