using ApiLibros.Models;
using System.Collections.Generic;

namespace ApiLibros.Repository.Irepsitory
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(int categoriID);

        bool ExisteCategoria(string nombre);
        bool ExisteCategoria(int id);

        bool CrearCategoria(Categoria categoria);
        bool AcualizarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoria);
        bool Guardar();



    }
}
