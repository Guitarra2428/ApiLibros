using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class CategoriaDto
    {
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
