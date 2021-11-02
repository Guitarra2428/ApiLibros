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
using System.IO;
using System.Linq;


namespace ApiLibros.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Libros")]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class LibrosController : ControllerBase
    {
        private readonly ILibroRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _iwebHost;

        public LibrosController(ILibroRepository repository, IMapper mapper, IWebHostEnvironment iwebHost)
        {
            _repository = repository;
            _mapper = mapper;
            _iwebHost = iwebHost;
        }
        /// <summary>
        /// Obtener Libros
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<LibroDto>))]
        [ProducesResponseType(400)]
        public IActionResult GuetLibros()
        {
            var datos = _repository.GetLibros();

            var datosDto = new List<LibroDto>();
            foreach (var lista in datos)
            {
                datosDto.Add(_mapper.Map<LibroDto>(lista));
            }

            return Ok(datosDto);
        }

        /// <summary>
        /// Obtener un Libro Individual
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{Id:int}", Name = "GuetLibro")]
        [ProducesResponseType(200, Type = typeof(LibroDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GuetLibro(int Id)
        {
            var datos = _repository.GetLibro(Id);
            if (datos == null)
            {
                return BadRequest(ModelState);
            }

            var datosDto = _mapper.Map<LibroDto>(datos);

            return Ok(datosDto);
        }
        /// <summary>
        /// Crear Libro
        /// </summary>
        /// <param name="libroCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateLibros([FromForm] LibroCreateDto libroCreateDto)
        {
            if (libroCreateDto == null)
            {
                return NotFound();
            }
            if (_repository.ExisteLibro(libroCreateDto.Titulo))
            {
                ModelState.AddModelError("", $"Este libro ya esta agregado{libroCreateDto.Titulo}");
                return StatusCode(404, ModelState);
            }

            //Subida de foto
            var archivo = libroCreateDto.Foto;
            var rutaPrincipal = _iwebHost.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (archivo.Length >= 0)
            {
                var nombreImagen = Guid.NewGuid().ToString();
                var subida = Path.Combine(rutaPrincipal, @"Imagenes");
                var extesion = Path.GetExtension(archivos[0].FileName);

                using (var Filestream = new FileStream(Path.Combine(subida, nombreImagen + extesion), FileMode.Create))
                {
                    archivos[0].CopyTo(Filestream);
                }

                libroCreateDto.UrlImagen = @"\Imagenes\" + nombreImagen + extesion;
            }


            var datosDto = _mapper.Map<Libro>(libroCreateDto);

            if (!_repository.CrearLibro(datosDto))
            {
                ModelState.AddModelError("", $"Este libro ya esta agregado {datosDto.Titulo}");
                return StatusCode(404, ModelState);
            }
            return CreatedAtRoute("GuetLibro", new { Id = datosDto.LibtoID }, datosDto);

        }
        /// <summary>
        /// Actualizar Libro
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="libroUpdateDto"></param>
        /// <returns></returns>
        [HttpPatch("{Id:int}", Name = "ActualizarLibros")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarLibros(int Id, [FromBody] LibroUpdateDto libroUpdateDto)
        {
            if (libroUpdateDto == null || Id != libroUpdateDto.LibtoID)
            {
                return BadRequest(ModelState);
            }

            var datosDto = _mapper.Map<Libro>(libroUpdateDto);

            if (!_repository.ActualizarLibro(datosDto))
            {
                ModelState.AddModelError("", $"Este libro no se  Actualizo {datosDto.Titulo}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        /// <summary>
        /// Buscar Libro Por Nombre
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
                var resultado = _repository.BuscarLibros(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NoContent();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error en la busqueda");
            }
        }
        /// <summary>
        /// Buscar Libro Por Autor Mediante Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        //[HttpGet("BuscarLibroPorAutor/{Id:int}")]
        //[ProducesResponseType(200, Type =typeof(List<LibroDto>))]
        //[ProducesResponseType(404)]
        //public IActionResult BuscarLibroPorAutor(int Id)
        //{
        //    var datosAutor = _repository.GetLibrosEnAutor(Id);
        //    if (datosAutor==null)
        //    {
        //        return NotFound();
        //    }

        //    var datosautorDto = new List<LibroDto>();

        //    foreach (var lista in datosAutor)
        //    {
        //        datosautorDto.Add(_mapper.Map<LibroDto>(lista));
        //    }

        //    return Ok(datosautorDto);
        //}
        /// <summary>
        /// Buscar libro por categoria mediante Id de categoria
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("BuscarLibroPorCategoria/{Id:int}")]
        [ProducesResponseType(200, Type = typeof(List<LibroDto>))]
        [ProducesResponseType(404)]
        public IActionResult BuscarLibroPorCategoria(int Id)
        {

            var datosAutor = _repository.GetLibrosEnCategoria(Id);
            if (datosAutor == null)
            {
                return NotFound();
            }

            var datosautorDto = new List<LibroDto>();

            foreach (var lista in datosAutor)
            {
                datosautorDto.Add(_mapper.Map<LibroDto>(lista));
            }

            return Ok(datosautorDto);
        }
        /// <summary>
        /// Borrar Libro
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}", Name = "GuetLibro")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarLibros(int Id)
        {
            var dato = _repository.GetLibro(Id);

            if (dato == null)
            {
                return NotFound();
            }

            if (!_repository.BorrarLibro(dato))
            {
                ModelState.AddModelError("", $"Este libro no se  Borro {dato.Titulo}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

}
