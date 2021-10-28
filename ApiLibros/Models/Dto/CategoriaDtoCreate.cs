using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class CategoriaDtoCreate
    {
       

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public String Nombre { get; set; }

    }
}
