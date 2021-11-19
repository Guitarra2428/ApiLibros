using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLibros.Models.Dto
{
    public class AutorDto
    {
        public int AutorId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public byte[] UrlImagen { get; set; }

        public int libroID { get; set; }

        [ForeignKey("libroID")]
        public Libro Libro { get; set; }
    }
}
