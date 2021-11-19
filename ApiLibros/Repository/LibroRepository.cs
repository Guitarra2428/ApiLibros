using ApiLibros.Data;
using ApiLibros.Models;
using ApiLibros.Repository.Irepsitory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiLibros.Repository
{
    public class LibroRepository : ILibroRepository
    {
        private readonly ApplicationDbContext _db;
        public LibroRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarLibro(Libro libro)
        {
            _db.Libros.Update(libro);
            return Guardar();
        }

        public bool BorrarLibro(Libro libro)
        {
            _db.Libros.Remove(libro);
            return Guardar();
        }

        public IEnumerable<Libro> BuscarLibros(string nombre)
        {
            IQueryable<Libro> query = _db.Libros;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(b => b.Titulo.Contains(nombre) || b.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearLibro(Libro libro)
        {
            _db.Libros.Add(libro);
            return Guardar();
        }

        public bool ExisteLibro(string nombre)
        {
            bool valor = _db.Libros.Any(n => n.Titulo.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteLibro(int id)
        {
            return _db.Libros.Any(e => e.LibroID == id);
        }

        public Libro GetLibro(int libroID)
        {
            return _db.Libros.FirstOrDefault(e => e.LibroID == libroID);

        }

        public ICollection<Libro> GetLibros()
        {
            return _db.Libros.OrderBy(l => l.Titulo).ToList();
        }

        //public ICollection<Libro> GetLibrosEnAutor(int autorId)
        //{
        //    return _db.Libros.Include(c => c.Autor).Where(a => a.autorID == autorId).ToList();
        //}

        public ICollection<Libro> GetLibrosEnCategoria(int categoriaId)
        {
            return _db.Libros.Include(c => c.Categoria).Where(a => a.categoriaID == categoriaId).ToList();

        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
