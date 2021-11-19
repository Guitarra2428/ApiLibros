using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLibros.Models
{
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class Autor
    {
        [Key]
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] UrlImagen { get; set; }

        public int libroID { get; set; }
        [ForeignKey("libroID")]
        public Libro Libro { get; set; }

    }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

}
