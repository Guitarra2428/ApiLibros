using ApiLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLibros.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }  

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Libro> Libros { get; set; }


    }
}
