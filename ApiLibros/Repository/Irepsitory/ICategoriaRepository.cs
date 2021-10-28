using ApiLibros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository.Irepsitory
{
  public  interface ICategoriaRepository
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
