using ApiLibros.Models;
using ApiLibros.Models.Dto;
using ApiLibros.Repository.Irepsitory;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiLibros.Controllers
{
    [Authorize]
    [Route("api/Autors")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LibrosAutor")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class AutorsController : ControllerBase
    {
        private readonly IAutorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _iwebHost;

        public AutorsController(IAutorRepository repository, IMapper mapper, IWebHostEnvironment iwebHost)
        {
            _repository = repository;
            _mapper = mapper;
            _iwebHost = iwebHost;
        }
        /// <summary>
        /// Obtenr los autores
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AutorDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAutors()
        {
            var datosAutors = _repository.GetAutors();

            var autorsDto = new List<AutorDto>();

            foreach (var lista in datosAutors)
            {
                autorsDto.Add(_mapper.Map<AutorDto>(lista));
            }

            return Ok(autorsDto);
        }
        /// <summary>
        /// Obtener autor individual
        /// </summary>
        /// <param name="Id"> Id Autor</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{Id:int}", Name = "GuetAutor")]
        [ProducesResponseType(200, Type = typeof(AutorDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GuetAutor(int Id)
        {
            var datos = _repository.GetAutor(Id);
            if (datos == null)
            {
                return BadRequest(ModelState);
            }

            var datosDto = _mapper.Map<Autor>(datos);

            return Ok(datosDto);
        }
        /// <summary>
        /// Crear  Autor
        /// </summary>
        /// <param name="autorCreateDto"></param>
        /// <returns></returns>

        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CrearAutor([FromBody] AutorDto autorCreateDto)
        {

            if (autorCreateDto == null)
            {
                return NotFound(ModelState);
            }
            if (_repository.ExisteAutor(autorCreateDto.Nombre + " " + autorCreateDto.Apellido))
            {
                ModelState.AddModelError("", $"El el Autor ya exixte{autorCreateDto.Nombre}");
                return StatusCode(404, ModelState);
            }

            

            var datosDto = _mapper.Map<Autor>(autorCreateDto);

            if (!_repository.CreateAutor(datosDto))
            {
                ModelState.AddModelError("", $"El el Autor no se agrego {datosDto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GuetAutor", new { Id = datosDto.AutorId }, datosDto);
        }
      
        [HttpPatch("{Id}", Name = "ActualizarAutor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarAutor(int Id, [FromBody] AutorDto autorUpdateDto)
        {
            if (autorUpdateDto == null || Id != autorUpdateDto.AutorId)
            {
                return NotFound();
            }

            var datoDto = _mapper.Map<Autor>(autorUpdateDto);

            if (!_repository.ActualizarAutor(datoDto))
            {
                ModelState.AddModelError("", $"El el Autor no se actualiso {datoDto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        /// <summary>
        /// Bus autor por Nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Buscar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _repository.BuscarAutor(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }

                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error en la busqueda");
            }
        }
        /// <summary>
        /// Borrar Autor
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>       
        [HttpDelete("{Id:int}", Name = "BorraAutor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorraAutor(int Id)
        {

            if (!_repository.ExisteAutor(Id))
            {
                return NotFound();
            }

            var dato = _repository.GetAutor(Id);

            if (!_repository.BorrarAutor(dato))
            {
                ModelState.AddModelError("", $"El el Autor no se borro {dato.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

}
