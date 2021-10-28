using ApiLibros.Data;
using ApiLibros.Models;
using ApiLibros.Repository.Irepsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Repository
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly ApplicationDbContext _Db;

        public UsuarioRepository(ApplicationDbContext db)
        {
            _Db = db;
        }

        public bool ExisteUsuario(string usuario)
        {
            if (_Db.Usuarios.Any(x=>x.UsuariA==usuario))
            {
                return true;
            }
            return false;
        }

        public Usuario GetUsuario(int usuarioId)
        {      
            return _Db.Usuarios.FirstOrDefault(x => x.Id == usuarioId);            
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _Db.Usuarios.OrderBy(x => x.UsuariA).ToList();

        }

        public bool Guardar()
        {
            return _Db.SaveChanges() >= 0 ? true : false;
        }

        public Usuario Login(string usuario, string Password)
        {
            var use = _Db.Usuarios.FirstOrDefault(x => x.UsuariA == usuario);
            if (use==null)
            {
                return null;
            }

            if (!VerificacionPasswordHash(Password, use.PasswordHash,  use.PasswordSalt))
            {
                return null;
            }
            return use;
        }

        public Usuario Registe(Usuario usuario, string Password)
        {
            byte[] passwordHash, passwordSalt;

            CrearPasswordHash(Password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _Db.Usuarios.Add(usuario);
            Guardar();
            return usuario;
        }

        private bool VerificacionPasswordHash( string password , byte[] passworHash,  byte[] passworSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(passworSalt))
            {
                var hasComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i <hasComputado.Length; i++)
                {
                    if (hasComputado[i]!=passworHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void CrearPasswordHash(string password, out byte[] passworHash, out byte[] passworSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passworSalt = hmac.Key;
                passworHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
            }
        }
    }
}

