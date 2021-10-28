﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Models.Dto
{
    public class LibroUpdateDto
    {

        public int LibtoID { get; set; }

        [Required(ErrorMessage = "Este dato es obligatorio")]
        public String Titulo { get; set; }
        public String Descripcion { get; set; }

        public DateTime FechaLanzamiento { get; set; }
        public String UrlImagen { get; set; }
        public IFormFile Foto { get; set; }

        public int categoriaID { get; set; }      

        public int autorID { get; set; }
        

    }
}