using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Azure
{
    public class UsuarioRolAzure
    {
        static string connectionString = "Server=DESKTOP-UJCISGT;Database=ApiReviewPelicula;Trusted_Connection=True;";

        private static List<UsuarioRol> usuarioRol;


        public static List<UsuarioRol> ObtenerUsuarioRol()
        {
            var dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from UsuarioRol";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                usuarioRol = new List<UsuarioRol>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    UsuarioRol usuariorol = new UsuarioRol();
                    usuariorol.idUsuarioRol = int.Parse(dataTable.Rows[i]["idUsuarioRol"].ToString());
                    usuariorol.idRol = int.Parse(dataTable.Rows[i]["idRol"].ToString());
                    usuariorol.idUsuario = int.Parse(dataTable.Rows[i]["idUsuario"].ToString());


                    usuarioRol.Add(usuariorol);
                }
                return usuarioRol;
            }
        }

        public static int AgregarUsuarioRol(UsuarioRol ur)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "insert into UsuarioRol values(@idUsuarioRol, @idRol, @idUsuario)";
                sqlCommand.Parameters.AddWithValue("@idUsuarioRol", ur.idUsuarioRol);
                sqlCommand.Parameters.AddWithValue("@idRol", ur.idRol);
                sqlCommand.Parameters.AddWithValue("@idUsuario", ur.idUsuario);

                try
                {
                    connection.Open();
                    insert = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>>>>>>" + ex.GetType());
                    Console.WriteLine(">>>>>>>>" + ex.Message);
                }
            }
            return insert;
        }

    }

}



