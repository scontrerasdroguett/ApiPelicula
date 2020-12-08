using Microsoft.AspNetCore.Mvc;
using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReviewPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        [HttpGet("all")]
        public JsonResult ObtenerPelicula()
        {
            var peliculasRecibidas = PeliculaAzure.ObtenerPelicula();
            return new JsonResult(peliculasRecibidas);
        }

        [HttpGet("{id}")]
        public JsonResult ObtenerPeliculasPorId(int id)
        {
            Pelicula pelicula = PeliculaAzure.ObtenerPeliculaPorId(id);
            if (pelicula is null)
            {
                return new JsonResult("No existe una pelicula registrada con esa ID");
            }
            else
            {
                return new JsonResult(pelicula);
            }
        }
        [HttpPost("Agregar")]
        public void AgregarPelicula([FromBody] Pelicula pelicula)
        {
            PeliculaAzure.AgregarPelicula(pelicula);
        }

        [HttpPut("Actualizar")]
        public void ActualizarPelicula([FromBody] Pelicula pelicula)
        {
            PeliculaAzure.ActualizarPelicula(pelicula);
        }

        [HttpDelete("Eliminar/{id}")]
        public void EliminarPelicula(int id)
        {
            PeliculaAzure.EliminarPelicula(id);
        }
    }
}
