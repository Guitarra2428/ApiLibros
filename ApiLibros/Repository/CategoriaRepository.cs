using ApiLibros.Data;
using ApiLibros.Models;
using ApiLibros.Repository.Irepsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AcualizarCategoria(Categoria categoria)
        {
            _db.Categorias.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _db.Categorias.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _db.Categorias.Add(categoria);
            return Guardar(); ;
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _db.Categorias.Any(e => e.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteCategoria(int id)
        {
            return _db.Categorias.Any(e => e.CategoriaID==id);

        }

        public Categoria GetCategoria(int categoriID)
        {
            return _db.Categorias.FirstOrDefault(c => c.CategoriaID == categoriID);

        }

        public ICollection<Categoria> GetCategorias()
        {
           return _db.Categorias.OrderBy(n => n.Nombre).ToList();

        }

        public bool Guardar()
        {
           return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
