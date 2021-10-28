using ApiLibros.Models;
using ApiLibros.Models.Dto;
using ApiLibros.Repository;
using ApiLibros.Repository.Irepsitory;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibros.Controllers
{
    [Authorize]
    [Route("api/Usuarios")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LibrosUsuario")]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuariosController(IUsuarioRepository repository, IMapper mapper, IConfiguration config)
        {
            _repository = repository;
            _mapper = mapper;
            _config = config;
        }
        /// <summary>
        /// Obtener Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UsuarioDto>))]
        [ProducesResponseType(400)]
        public  IActionResult GetUsuarios()
        {
           var listadatos= _repository.GetUsuarios();

            var listadoUsuariosDto = new List<UsuarioDto>();

            foreach (var lista in listadatos)
            {
                listadoUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }
            return Ok(listadoUsuariosDto);
        }
        /// <summary>
        /// Obtener Usuario Individual
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("{usuarioId:int}", Name = "GuetUsuario")]
        [ProducesResponseType(200, Type = typeof(AutorDto))]
        [ProducesResponseType(404)]
        public IActionResult GuetUsuario(int usuarioId)
        {
           var usuario= _repository.GetUsuario(usuarioId);

            if (usuario==null)
            {
                return NotFound(ModelState);
            }

            var UsuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(UsuarioDto);
        }
        /// <summary>
        /// Registro de usuario
        /// </summary>
        /// <param name="usuaroCreateDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
      [HttpPost ("Registro")]
      public IActionResult Registro(UsuaroCreateDto usuaroCreateDto)
        {
            usuaroCreateDto.Usuario = usuaroCreateDto.Usuario.ToLower();

            if (_repository.ExisteUsuario(usuaroCreateDto.Usuario))
            {
                return BadRequest("El Usario ya exixte");
            }

            var UsuarioAcrear = new Usuario
            {
                UsuariA = usuaroCreateDto.Usuario
            };

            var usuarioCreado = _repository.Registe(UsuarioAcrear, usuaroCreateDto.Password);

            return Ok(usuarioCreado);

        }
        /// <summary>
        /// Login De usuario
        /// </summary>
        /// <param name="usuarioLoginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UsuarioLoginDto usuarioLoginDto)
        {

            var datosDesdeDb = _repository.Login(usuarioLoginDto.Usuario, usuarioLoginDto.Password);

            if (datosDesdeDb == null)
            {
                return Unauthorized();
            }


            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, datosDesdeDb.Id.ToString()),
                new Claim(ClaimTypes.Name, datosDesdeDb.UsuariA)
            };

            //geracion del token

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDecripto = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credencial
            };

            var tokenhadler = new JwtSecurityTokenHandler();
            var Token = tokenhadler.CreateToken(tokenDecripto);

            return Ok(new 
            {
                Token = tokenhadler.WriteToken(Token)
            });

        }
    }
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

}
