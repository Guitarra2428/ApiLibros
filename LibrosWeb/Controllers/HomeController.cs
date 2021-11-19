using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using LibrosWeb.Utilidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrosWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAcounRepository _acountRepositoryr;

        public HomeController(ILogger<HomeController> logger, IAcounRepository acountRepositoryr)
        {
            _logger = logger;
            _acountRepositoryr = acountRepositoryr;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            UsuarioM usuarioM = new UsuarioM();
            return View(usuarioM);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioM usuarioM)
        {
            var login = await _acountRepositoryr.LoginAsync(CT.UrlUsuario+ "Login", usuarioM);
            if (login == null)
            {
                TempData["error"] = "Los datos son incorrecto";
                return View();

            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, usuarioM.Usuario));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", usuarioM.Usuario);
            TempData["error"] = "Bienvenido" + usuarioM.Usuario;
            return RedirectToAction("Index");


        }
        public  IActionResult Registro()
        {
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(UsuarioM usuarioM)
        {
            var regist = await _acountRepositoryr.RegistroAsync(CT.UrlUsuario +"Registro", usuarioM);
             if (regist == false)
             {
                return View();

             }
            TempData["error"] = "Registro correcto";
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logaut()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken","");
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
