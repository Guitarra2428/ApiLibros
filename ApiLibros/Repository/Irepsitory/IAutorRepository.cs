using ApiLibros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository.Irepsitory
{
 public   interface IAutorRepository
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
