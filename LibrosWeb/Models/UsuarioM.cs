using System.ComponentModel.DataAnnotations;

namespace LibrosWeb.Models
{
    public class UsuarioM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

    }
}
