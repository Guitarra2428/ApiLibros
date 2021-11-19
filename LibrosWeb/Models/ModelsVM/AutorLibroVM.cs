using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LibrosWeb.Models.ModelsVM
{
    public class AutorLibroVM
    {
        public IEnumerable<SelectListItem> ListaLibro { get; set; }
        public Autor Autor { get; set; }
    }
}
