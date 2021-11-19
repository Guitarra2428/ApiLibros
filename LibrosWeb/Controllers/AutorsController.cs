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
    public class AutorsController : Controller
    {
        private readonly IAutorRepository _repository;
        private readonly ILibroRepository _repositoryLibro;


        public AutorsController(IAutorRepository repository, ILibroRepository repositoryLibro)
        {
            _repository = repository;
            _repositoryLibro = repositoryLibro;
        }

        public IActionResult Index()
        {
            return View(new Autor() { });
        }


        [HttpGet]
        public async Task<IActionResult> GetTodasAutors()
        {
            return Json(new { data = await _repository.GetTodosAsync(CT.UrlApiAutor) });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Libro> librosLista = (IEnumerable<Libro>)await _repositoryLibro.GetTodosAsync(CT.UrlApiLibro);

            AutorLibroVM autorLibroVM = new AutorLibroVM()
            {
                ListaLibro = librosLista.Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.LibroID.ToString()
                }),
                Autor = new Autor()

            };
            return View(autorLibroVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Autor autor)
        {
            IEnumerable<Libro> librosLista = (IEnumerable<Libro>)await _repositoryLibro.GetTodosAsync(CT.UrlApiLibro);

            AutorLibroVM autorLibroVM = new AutorLibroVM()
            {
                ListaLibro = librosLista.Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.LibroID.ToString()
                }),
                Autor = new Autor()

            };

            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                if (archivo.Count >= 0)
                {
                    byte[] imagen = null;
                    using (var file = archivo[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            imagen = ms.ToArray();
                        }
                        autor.UrlImagen = imagen;
                    }
                }

                else
                {
                    return View(autorLibroVM);

                }

                await _repository.CrearAsync(CT.UrlApiAutor, autor , HttpContext.Session.GetString("JWToken"));
                return RedirectToAction("Index");
            }

            return View(autorLibroVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            IEnumerable<Libro> AutorsList = (IEnumerable<Libro>)await _repositoryLibro.GetTodosAsync(CT.UrlApiLibro);

            AutorLibroVM autorLibroVM = new AutorLibroVM()
            {
                ListaLibro = AutorsList.Select(x => new SelectListItem
                {
                    Text = x.Titulo,
                    Value = x.LibroID.ToString()
                }),
                Autor = new Autor()

            };
            if (id == null)
            {
                return NotFound();
            }

            autorLibroVM.Autor = await _repository.GetTAsync(CT.UrlApiAutor, id.GetValueOrDefault());
            if (autorLibroVM.Autor == null)
            {

                return NotFound();
            }

            return View(autorLibroVM);

        }


        [HttpPost]
        public async Task<IActionResult> Update(Autor autor)
        {

            if (ModelState.IsValid)
            {
                var archivo = HttpContext.Request.Form.Files;
                if (archivo.Count > 0)
                {
                    byte[] imagen = null;
                    using (var file = archivo[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            imagen = ms.ToArray();
                        }
                        autor.UrlImagen = imagen;
                    }
                }
                else
                {
                    var datodb = await _repository.GetTAsync(CT.UrlApiAutor, autor.AutorId);
                    autor.UrlImagen = datodb.UrlImagen;

                }

                await _repository.ActualizarAsync(CT.UrlApiAutor + autor.AutorId, autor, HttpContext.Session.GetString("JWToken"));
                return RedirectToAction("Index");
            }

            return View();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var AutorsItem = await _repository.BorrarAsync(CT.UrlApiAutor, id, HttpContext.Session.GetString("JWToken"));

            if (AutorsItem)
            {
                return Json(new { success = true, message = "El Registro Se Borado Correctamente" });
            }

            return Json(new { success = true, message = "El Registro Se Borado Correctamente" });


        }
    }
}
