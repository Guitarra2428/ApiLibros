using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLibros.Models
{
    public class Libro
    {
        [Key]
        public int LibtoID { get; set; }
        public String Titulo { get; set; }
        public String Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public String UrlImagen { get; set; }

        public int categoriaID { get; set; }
        [ForeignKey("categoriaID")]
        public Categoria Categoria { get; set; }


    }
}
