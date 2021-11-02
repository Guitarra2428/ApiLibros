using ApiLibros.Data;
using ApiLibros.Models;
using ApiLibros.Repository.Irepsitory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiLibros.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly ApplicationDbContext _db;

        public AutorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ActualizarAutor(Autor autor)
        {
            _db.Autors.Update(autor);
            return Guardar();
        }

        public bool BorrarAutor(Autor autor)
        {
            _db.Autors.Remove(autor);
            return Guardar();
        }

        public IEnumerable<Autor> BuscarAutor(string nombre)
        {
            IQueryable<Autor> query = _db.Autors;

            if (!String.IsNullOrEmpty(nombre))
            {
                query = query.Where(B => B.Nombre.Contains(nombre) || B.Apellido.Contains(nombre));
            }

            return query.ToList();
        }

        public bool CreateAutor(Autor autor)
        {
            _db.Autors.Add(autor);
            return Guardar();
        }

        public bool ExisteAutor(int autorID)
        {
            return _db.Autors.Any(A => A.AutorId == autorID);
        }

        public bool ExisteAutor(string nombre)
        {
            bool valor = _db.Autors.Any(A => A.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

            return valor;

        }

        public Autor GetAutor(int autorID)
        {
            return _db.Autors.FirstOrDefault(A => A.AutorId == autorID);

        }

        public ICollection<Autor> GetAutors()
        {
            return _db.Autors.OrderBy(n => n.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
