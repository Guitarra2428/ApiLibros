using System.ComponentModel.DataAnnotations;

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
