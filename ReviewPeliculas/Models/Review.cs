using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Models
{
    public class Review
    {
        public int idReview { get; set; }
        public int idPelicula { get; set; }
        public string descripcion { get; set; }

    }
}
