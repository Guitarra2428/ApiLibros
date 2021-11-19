using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using LibrosWeb.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibrosWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _repository;

        public UsuariosController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(new UsuarioU() { });
        }


        [HttpGet]
        public async Task<IActionResult> GetTodasUsuarios()
        {
            return Json(new { data = await _repository.GetTodosAsync(CT.UrlUsuario) });
        }



    }
}
