using Microsoft.AspNetCore.Http;
using System;

namespace ApiLibros.Models.Dto
{
    public class AutorDto
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String UrlImagen { get; set; }
        public IFormFile Foto { get; set; }


    }
}
