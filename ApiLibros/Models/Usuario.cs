namespace ApiLibros.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UsuariA { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
