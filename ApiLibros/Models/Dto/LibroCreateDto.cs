using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class LibroCreateDto
    {

        [Required(ErrorMessage = "Este dato es obligatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public byte[] UrlImagen { get; set; }

        public int categoriaID { get; set; }
    }
}
