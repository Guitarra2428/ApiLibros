using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLibros.Models
{
    public class Libro
    {
        [Key]
        public int LibroID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public byte[] UrlImagen { get; set; }

        public int categoriaID { get; set; }
        [ForeignKey("categoriaID")]
        public Categoria Categoria { get; set; }


    }
}
