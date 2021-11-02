using ApiLibros.Models;
using ApiLibros.Models.Dto;
using ApiLibros.Repository.Irepsitory;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiLibros.Controllers
{
    [Authorize]
    [Route("api/Categorias")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LibrosCategoria")]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtener las Categorias
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoriaDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategorias()
        {
            var listadatos = _repository.GetCategorias();

            var listadoCategoriaDto = new List<CategoriaDto>();

            foreach (var lista in listadatos)
            {
                listadoCategoriaDto.Add(_mapper.Map<CategoriaDto>(lista));
            }
            return Ok(listadoCategoriaDto);
        }

        /// <summary>
        /// Obtener Categoria Individual
        /// </summary>
        /// <param name="categoriaId"> Id Categoria </param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{categoriaId:int}", Name = "GuetCategoria")]
        [ProducesResponseType(200, Type = typeof(CategoriaDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GuetCategoria(int categoriaId)
        {
            var categoria = _repository.GetCategoria(categoriaId);

            if (categoria == null)
            {
                return NotFound(ModelState);
            }

            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);

            return Ok(categoriaDto);
        }
        /// <summary>
        /// Crear Categoria
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CrearCategoria([FromBody] CategoriaDtoCreate categoriaDto)
        {
            if (categoriaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_repository.ExisteCategoria(categoriaDto.Nombre))
            {
                ModelState.AddModelError("", $"La CATEGORIA YA EXISTE");
                return StatusCode(404, ModelState);
            }

            var datoCategoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_repository.CrearCategoria(datoCategoria))
            {
                ModelState.AddModelError(" ", $"Error al agregar nuevo dato {datoCategoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GuetCategoria", new { categoriaId = datoCategoria.CategoriaID }, datoCategoria);
        }
        /// <summary>
        /// Actualizar Categoria
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPatch("{categoriaId:int}", Name = "ActualizarCategoria")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDto)
        {
            if (categoriaDto == null || categoriaId != categoriaDto.CategoriaID)
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_repository.AcualizarCategoria(categoria))
            {
                ModelState.AddModelError(" ", $"Error al Actulizar nuevo dato {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Borrar Categoria
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarCategoria(int categoriaId)
        {
            var valor = _repository.GetCategoria(categoriaId);

            if (valor == null)
            {
                return NotFound();

            }

            if (!_repository.BorrarCategoria(valor))
            {
                ModelState.AddModelError(" ", $"Error al borrar  dato {valor.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

}
