﻿using LibrosWeb.Models;
using LibrosWeb.Repository.IRepository;
using LibrosWeb.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibrosWeb.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(new Categoria() { });
        }

        [HttpGet]
        public IActionResult GetTodasCategorias()
        {
            return Json(new { data = _repository.GetTodosAsync(CT.UrApiCategoria) });
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task< IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _repository.CrearAsync(CT.UrApiCategoria, categoria);
                return RedirectToAction(nameof(Index));
            }

             return View( );

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            Categoria categoriaItem = new Categoria();
            if (id==null)
            {
                return NotFound();
            }

            categoriaItem = await _repository.GetTAsync(CT.UrApiCategoria, id.GetValueOrDefault());
            if (categoriaItem==null)
            {
                return NotFound();
            }

            return View(categoriaItem);

        }


        [HttpPost]
        public async Task<IActionResult> Udate(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await _repository.ActualizarAsync(CT.UrApiCategoria + categoria.CategoriaID, categoria);
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {            
            var categoriaItem = await  _repository.BorrarAsync(CT.UrApiCategoria , id);

            if (categoriaItem)
            {
                return Json(new {success=true, message="El Registro Se Borado Correctamente" });
            }

            return Json(new { success = true, message = "El Registro Se Borado Correctamente" });


        }
    }
}
