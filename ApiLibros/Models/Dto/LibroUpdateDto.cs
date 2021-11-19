using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class LibroUpdateDto
    {

        public int LibroID { get; set; }

        [Required(ErrorMessage = "Este dato es obligatorio")]
        public String Titulo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public byte[] UrlImagen { get; set; }
        //public IFormFile Foto { get; set; }

        public int categoriaID { get; set; }



    }
}
