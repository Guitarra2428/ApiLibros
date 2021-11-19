namespace ApiLibros.Models.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        public string UsuariA { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
