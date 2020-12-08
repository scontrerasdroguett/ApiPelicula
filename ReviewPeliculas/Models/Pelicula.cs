using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Models
{
    public class Pelicula
    {
        public int idPelicula { get; set; }
        public int idUsuario { get; set; }
        public string titulo { get; set; }
        public string sinopsis { get; set; }
        public string actor { get; set; }
        public string director { get; set; }
        public string categoria { get; set; }
        public string idioma { get; set; }
    }
}

