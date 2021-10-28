using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLibros.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }
        public String Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
