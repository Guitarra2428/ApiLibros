using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dto
{
    public class LibroDto
    {

        public int LibtoID { get; set; }
        public String Titulo { get; set; }
        public String Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public String UrlImagen { get; set; }
        public IFormFile Foto { get; set; }


    }
}
