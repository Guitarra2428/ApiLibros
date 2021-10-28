using ApiLibros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository.Irepsitory
{
 public   interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int  usuarioId);
        bool ExisteUsuario(string usuario);
        Usuario Registe(Usuario usuario, string Password);
        Usuario Login(string usuario, string Password);
        bool Guardar();
    }
}
