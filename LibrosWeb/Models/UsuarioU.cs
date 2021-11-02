using System.ComponentModel.DataAnnotations;

namespace LibrosWeb.Models
{
    public class UsuarioU
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string UsuariA { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
