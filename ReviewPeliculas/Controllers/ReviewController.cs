using Microsoft.AspNetCore.Mvc;
using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReviewPeli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        // GET: api/<ReviewController>/all
        [HttpGet("all")]
        public JsonResult ObtenerReviews()
        {
            var reviewsRecibidas = ReviewAzure.ObtenerReviews();
            return new JsonResult(reviewsRecibidas);
        }

        [HttpGet("{id}")]
        public JsonResult ObtenerReviewsId(int id)
        {
            Review review = ReviewAzure.ObtenerReviewPorId(id);
            if (review is null)
            {
                return new JsonResult("No existe una review asociada a esa ID");
            }
            else
            {
                return new JsonResult(review);
            }
        }

        [HttpPost("Agregar")]
        public void AgregarReview([FromBody] Review review)
        {
            ReviewAzure.AgregarReview(review);
        }

        [HttpPut("Actualizar")]
        public void ActualizarReview([FromBody] Review review)
        {
            ReviewAzure.ActualizarReview(review);
        }

        [HttpDelete("Eliminar/{id}")]
        public void EliminarReview(int id)
        {
            ReviewAzure.EliminarReview(id);
        }
    }
}
