using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Azure
{
    public class RolAzure
    {

        //static string connectionString = "Server=DESKTOP-UJCISGT;Database=ApiReviewPelicula;Trusted_Connection=True;";
	static string connectionString = "Server=tcp:apipelicula.database.windows.net,1433;Database=APIPELICULA;User ID=adminapi;Password=Abcd1234;Trusted_Connection=False;Encrypt=True;";

        private static List<Rol> rol;


        public static List<Rol> ObtenerRol()
        {
            var dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from Rol";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                rol = new List<Rol>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Rol roles = new Rol();
                    roles.idRol = int.Parse(dataTable.Rows[i]["idRol"].ToString());
                    roles.nombre = dataTable.Rows[i]["nombre"].ToString();


                    rol.Add(roles);
                }
                return rol;
            }
        }
    }
}
