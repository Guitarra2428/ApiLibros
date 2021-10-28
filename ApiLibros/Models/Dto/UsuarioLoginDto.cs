using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dto
{
    public class UsuarioLoginDto
    {

        [Required(ErrorMessage = "Estecampo es obligatorio")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Estecampo es obligatorio")]
        public string Password { get; set; }
    }
}
