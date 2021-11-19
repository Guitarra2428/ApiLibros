using LibrosWeb.Models;
using LibrosWeb.Models.ModelsVM;
using LibrosWeb.Repository.IRepository;
using LibrosWeb.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibrosWeb.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ICategoriaRepository _repositoryCategoria;
        private readonly ILibroRepository _repositoryLibro;


        public LibrosController(ICategoriaRepository repository, ILibroRepository repositoryLibro)
        {
            _repositoryCategoria = repository;
            _repositoryLibro = repositoryLibro;
        }

        public IActionResult Index()
        {
            return View(new Libro() { });
        }


        [HttpGet]
        public async Task<IActionResult> GetTodasLibros()
        {
            return Json(new { data = await _repositoryLibro.GetTodosAsync(CT.UrlApiLibro) });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Categoria> categoriaList = (IEnumerable<Categoria>)await _repositoryCategoria.GetTodosAsync(CT.UrApiCategoria);

            LibroCategoriaVM autorLibroVM = new LibroCategoriaVM()
            {
                ListaCategoria = categoriaList.Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.CategoriaID.ToString()
                }),
                Libro = new Libro()

            };
            return View(autorLibroVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            IEnumerable<Categoria> categoriaList = (IEnumerable<Categoria>)await _repositoryCategoria.GetTodosAsync(CT.UrApiCategoria);

            LibroCategoriaVM autorLibroVM = new LibroCategoriaVM()
            {
                ListaCategoria = categoriaList.Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.CategoriaID.ToString()
                }),
                Libro = new Libro()

            };


            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;

                if (archivos.Count >= 0)
                {
                    byte[] p1 = null;
                    using (var archivo1 = archivos[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            archivo1.CopyTo(ms);
                            p1 = ms.ToArray();
                        }
                    }
                    libro.UrlImagen = p1;
                }

                await _repositoryLibro.CrearAsync(CT.UrlApiLibro, libro, HttpContext.Session.GetString("JWToken"));
                return RedirectToAction("Index");
            }

            return View(autorLibroVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            IEnumerable<Categoria> categoriaList = (IEnumerable<Categoria>)await _repositoryCategoria.GetTodosAsync(CT.UrApiCategoria);

            LibroCategoriaVM autorLibroVM = new LibroCategoriaVM()
            {
                ListaCategoria = categoriaList.Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.CategoriaID.ToString()
                }),
                Libro = new Libro()

            };
            if (id == null)
            {
                return NotFound();
            }

            autorLibroVM.Libro = await _repositoryLibro.GetTAsync(CT.UrlApiLibro, id.GetValueOrDefault());
            if (autorLibroVM.Libro == null)
            {

                return NotFound();
            }

            return View(autorLibroVM);

        }


        [HttpPost]
        public async Task<IActionResult> Update(Libro libro)
        {


            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;

                if (archivos.Count > 0)
                {
                    byte[] p1 = null;
                    using (var archivo1 = archivos[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            archivo1.CopyTo(ms);
                            p1 = ms.ToArray();
                        }
                    }
                    libro.UrlImagen = p1;
                }
                else
                {
                    var datosDb = await _repositoryLibro.GetTAsync(CT.UrlApiLibro, libro.LibroID);
                    libro.UrlImagen = datosDb.UrlImagen;
                }

                await _repositoryLibro.ActualizarAsync(CT.UrlApiLibro + libro.LibroID, libro, HttpContext.Session.GetString("JWToken"));
                return RedirectToAction(nameof(Index));
            }

            return View();


        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var AutorsItem = await _repositoryLibro.BorrarAsync(CT.UrlApiLibro, id, HttpContext.Session.GetString("JWToken"));

            if (AutorsItem)
            {
                return Json(new { success = true, message = "El Registro Se Borado Correctamente" });
            }

            return Json(new { success = true, message = "El Registro Se Borado Correctamente" });


        }
    }
}
