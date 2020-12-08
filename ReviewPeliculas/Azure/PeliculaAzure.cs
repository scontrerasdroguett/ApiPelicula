using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Azure
{
    public class PeliculaAzure
    {
        //static string connectionString = "Server=DESKTOP-UJCISGT;Database=ApiReviewPelicula;Trusted_Connection=True;";
	static string connectionString = "Server=tcp:apipelicula.database.windows.net,1433;Database=APIPELICULA;User ID=adminapi;Password=Abcd1234;Trusted_Connection=False;Encrypt=True;";
        private static List<Pelicula> peli;

        public static List<Pelicula> ObtenerPelicula()
        {
            var dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from Pelicula";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                peli = new List<Pelicula>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.idPelicula = int.Parse(dataTable.Rows[i]["idPelicula"].ToString());
                    pelicula.idUsuario = int.Parse(dataTable.Rows[i]["idUsuario"].ToString());
                    pelicula.titulo = dataTable.Rows[i]["titulo"].ToString();
                    pelicula.sinopsis = dataTable.Rows[i]["sinopsis"].ToString();
                    pelicula.actor = dataTable.Rows[i]["actor"].ToString();
                    pelicula.director = dataTable.Rows[i]["director"].ToString();
                    pelicula.categoria = dataTable.Rows[i]["categoria"].ToString();
                    pelicula.idioma = dataTable.Rows[i]["idioma"].ToString();
                    peli.Add(pelicula);
                }
                return peli;
            }
        }

        //Obtener review por ID
        public static Pelicula ObtenerPeliculaPorId(int idPelicula)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //1.- Creación y apertura de conexión
                var comando = ConsultaPeliculaPorId(connection, idPelicula);

                //2.- Llenado de datatable y conversión
                var dataTable = LlenarDataTable(comando);

                //3.- Crear y retornar objeto review
                return CreacionPelicula(dataTable);
            }
        }

        //Agregar pelicula 
        public static int AgregarPelicula(Pelicula pelicula)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "insert into Pelicula values(@idPelicula, @idUsuario, @titulo, @sinopsis, @actor, @director, @categoria, @idioma)";
                sqlCommand.Parameters.AddWithValue("@idPelicula", pelicula.idPelicula);
                sqlCommand.Parameters.AddWithValue("@idUsuario", pelicula.idUsuario);
                sqlCommand.Parameters.AddWithValue("@titulo", pelicula.titulo);
                sqlCommand.Parameters.AddWithValue("@sinopsis", pelicula.sinopsis);
                sqlCommand.Parameters.AddWithValue("@actor", pelicula.actor);
                sqlCommand.Parameters.AddWithValue("@director", pelicula.director);
                sqlCommand.Parameters.AddWithValue("@categoria", pelicula.categoria);
                sqlCommand.Parameters.AddWithValue("@idioma", pelicula.idioma);
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

        //Eliminar Review
        public static int EliminarPelicula(int idPelicula)
        {
            int delete = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "delete from Pelicula where idPelicula = @idPelicula";
                sqlCommand.Parameters.AddWithValue("@idPelicula", idPelicula);
                try
                {
                    connection.Open();
                    delete = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">>>>>>>>" + ex.GetType());
                    Console.WriteLine(">>>>>>>>" + ex.Message);
                }
            }
            return delete;
        }

        //Actualizar review 
        public static int ActualizarPelicula(Pelicula pelicula)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "update Pelicula set titulo = @titulo, sinopsis = @sinopsis, actor = @actor, director = @director, categoria = @categoria, idioma = @idioma where idPelicula = @idPelicula";
                sqlCommand.Parameters.AddWithValue("@idPelicula", pelicula.idPelicula);
                sqlCommand.Parameters.AddWithValue("@idUsuario", pelicula.idUsuario);
                sqlCommand.Parameters.AddWithValue("@titulo", pelicula.titulo);
                sqlCommand.Parameters.AddWithValue("@sinopsis", pelicula.sinopsis);
                sqlCommand.Parameters.AddWithValue("@actor", pelicula.actor);
                sqlCommand.Parameters.AddWithValue("@director", pelicula.director);
                sqlCommand.Parameters.AddWithValue("@categoria", pelicula.categoria);
                sqlCommand.Parameters.AddWithValue("@idioma", pelicula.idioma);
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

        private static Pelicula CreacionPelicula(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Pelicula pelicula = new Pelicula();
                pelicula.idPelicula = int.Parse(dataTable.Rows[0]["idPelicula"].ToString());
                pelicula.idUsuario = int.Parse(dataTable.Rows[0]["idUsuario"].ToString());
                pelicula.titulo = dataTable.Rows[0]["titulo"].ToString();
                pelicula.sinopsis = dataTable.Rows[0]["sinopsis"].ToString();
                pelicula.actor = dataTable.Rows[0]["actor"].ToString();
                pelicula.director = dataTable.Rows[0]["director"].ToString();
                pelicula.categoria = dataTable.Rows[0]["categoria"].ToString();
                pelicula.idioma = dataTable.Rows[0]["idioma"].ToString();
                return pelicula;
            }
            else
            {
                return null;
            }
        }

        private static DataTable LlenarDataTable(SqlCommand comando)
        {
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        private static SqlCommand ConsultaPeliculaPorId(SqlConnection connection, int idPelicula)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = $"select * from Pelicula where idPelicula = {idPelicula}";
            connection.Open();
            return sqlCommand;
        }
    }
}
