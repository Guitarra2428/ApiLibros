using System;
using System.ComponentModel.DataAnnotations;

namespace LibrosWeb.Models
{
    public class Libro
    {

        public int LibroID { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public byte[] UrlImagen { get; set; }

        public int categoriaID { get; set; }
        public Categoria Categoria { get; set; }

    }
}
