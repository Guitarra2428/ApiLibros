using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models.Dto
{
    public class UsuaroCreateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Estecampo es obligatorio")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Estecampo es obligatorio")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 10 y 4 caracteres")]
        public string Password { get; set; }
    }
}
