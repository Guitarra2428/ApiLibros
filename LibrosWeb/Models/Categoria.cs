using System;
using System.ComponentModel.DataAnnotations;

namespace LibrosWeb.Models
{
    public class Categoria
    {

        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
