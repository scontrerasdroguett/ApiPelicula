using System;
using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReviewPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("all")]
        public JsonResult ObtenerUsuarios()
        {
            var usuariosRecibidos = UsuarioAzure.ObtenerUsuario();
            return new JsonResult(usuariosRecibidos);

        }

        [HttpGet("{id}")]
        public JsonResult ObtenerUsuariosId(int id)
        {
            Usuario usuario = UsuarioAzure.ObtenerUsuarioPorId(id);
            if (usuario is null)
            {
                return new JsonResult("No existen usarios registrados con esa ID");
            }
            else
            {
                return new JsonResult(usuario);
            }

        }

        //POST: api/usuario
        [HttpPost("Agregar")]
        public void AgregarUsuario([FromBody] Usuario usuario)
        {
            UsuarioAzure.AgregarUsuario(usuario);
        }

        [HttpDelete("Eliminar/{id}")]
        public void EliminarUsuario(int id)
        {
            UsuarioAzure.EliminarUsuario(id);
        }

        [HttpPut("Actualizar")]
        public void ActualizarUsuario([FromBody] Usuario usuario)
        {
            UsuarioAzure.ActualizarUsuario(usuario);
        }

    }


}
