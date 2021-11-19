using System;

namespace LibrosWeb.Models
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] UrlImagen { get; set; }

        public int libroID { get; set; }
        public Libro Libro { get; set; }
    }
}
