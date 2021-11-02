using ApiLibros.Models;
using System.Collections.Generic;

namespace ApiLibros.Repository.Irepsitory
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool ExisteUsuario(string usuario);
        Usuario Registe(Usuario usuario, string Password);
        Usuario Login(string usuario, string Password);
        bool Guardar();
    }
}
