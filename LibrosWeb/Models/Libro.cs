using System;
using System.ComponentModel.DataAnnotations;

namespace LibrosWeb.Models
{
    public class Libro
    {

        public int LibtoID { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Titulo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public String UrlImagen { get; set; }

        public int categoriaID { get; set; }
        public Categoria Categoria { get; set; }

    }
}
