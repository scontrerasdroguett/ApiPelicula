using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        public string nombres { get; set; }

        public string apellidos { get; set; }

        public int edad { get; set; }

        public string genero { get; set; }

        public string email { get; set; }
    }


       }


