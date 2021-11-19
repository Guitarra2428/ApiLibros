using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LibrosWeb.Models.ModelsVM
{
    public class LibroCategoriaVM
    {
        public IEnumerable<SelectListItem> ListaCategoria { get; set; }
        public Libro Libro { get; set; }
    }
}
