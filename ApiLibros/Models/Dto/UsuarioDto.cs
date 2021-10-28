using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dto
{
  public  class UsuarioDto
    {
        public string UsuariA { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
