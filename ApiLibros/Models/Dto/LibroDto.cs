using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class LibroDto
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
        // [ForeignKey("categoriaID")]

    }
}
