using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class LibroCreateDto
    {

        [Required(ErrorMessage = "Este dato es obligatorio")]
        public String Titulo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Descripcion { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public String UrlImagen { get; set; }
        public IFormFile Foto { get; set; }

        public int categoriaID { get; set; }
    }
}
