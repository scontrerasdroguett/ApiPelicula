using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Azure
{
    public class UsuarioAzure
    {

        //static string connectionString = "Server=DESKTOP-UJCISGT;Database=ApiReviewPelicula;Trusted_Connection=True;";
	static string connectionString = "Server=tcp:apipelicula.database.windows.net,1433;Database=APIPELICULA;User ID=adminapi;Password=Abcd1234;Trusted_Connection=False;Encrypt=True;";

        private static List<Usuario> user;

        public static List<Usuario> ObtenerUsuario()
        {
            var dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from Usuario";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                user = new List<Usuario>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Usuario usuario = new Usuario();
                    usuario.idUsuario = int.Parse(dataTable.Rows[i]["idUsuario"].ToString());
                    usuario.nombres = dataTable.Rows[i]["nombres"].ToString();
                    usuario.apellidos = dataTable.Rows[i]["apellidos"].ToString();
                    usuario.edad = int.Parse(dataTable.Rows[i]["edad"].ToString());
                    usuario.genero = dataTable.Rows[i]["genero"].ToString();
                    usuario.email = dataTable.Rows[i]["email"].ToString();

                    user.Add(usuario);
                }
                return user;
            }
        }

        //Obtener review por ID
        public static Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //1.- Creación y apertura de conexión
                var comando = ConsultaUsuarioPorId(connection, idUsuario);

                //2.- Llenado de datatable y conversión
                var dataTable = LlenarDataTable(comando);

                //3.- Crear y retornar objeto review
                return CreacionUsuario(dataTable);
            }
        }

        //Agregar review 
        public static int AgregarUsuario(Usuario u)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "insert into Usuario values(@idUsuario, @nombres, @apellidos,@edad,@genero,@email)";
                sqlCommand.Parameters.AddWithValue("@idUsuario", u.idUsuario);
                sqlCommand.Parameters.AddWithValue("@nombres", u.nombres);
                sqlCommand.Parameters.AddWithValue("@apellidos", u.apellidos);
                sqlCommand.Parameters.AddWithValue("@edad", u.edad);
                sqlCommand.Parameters.AddWithValue("@genero", u.genero);
                sqlCommand.Parameters.AddWithValue("@email", u.email);
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
        public static int EliminarUsuario(int idUsuario)
        {
            int delete = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "delete from Usuario where idUsuario = @idUsuario";
                sqlCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
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
        public static int ActualizarUsuario(Usuario u)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "update Usuario set nombres = @nombres, apellidos = @apellidos, edad = @edad, genero = @genero, email = @email where idUsuario = @idUsuario";
                sqlCommand.Parameters.AddWithValue("@idUsuario", u.idUsuario);
                sqlCommand.Parameters.AddWithValue("@nombres", u.nombres);
                sqlCommand.Parameters.AddWithValue("@apellidos", u.apellidos);
                sqlCommand.Parameters.AddWithValue("@edad", u.edad);
                sqlCommand.Parameters.AddWithValue("@genero", u.genero);
                sqlCommand.Parameters.AddWithValue("@email", u.email);
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

        private static Usuario CreacionUsuario(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Usuario usuario = new Usuario();
                usuario.idUsuario = int.Parse(dataTable.Rows[0]["idUsuario"].ToString());
                usuario.nombres = dataTable.Rows[0]["nombres"].ToString();
                usuario.apellidos = dataTable.Rows[0]["apellidos"].ToString();
                usuario.edad = int.Parse(dataTable.Rows[0]["edad"].ToString());
                usuario.genero = dataTable.Rows[0]["genero"].ToString();
                usuario.email = dataTable.Rows[0]["email"].ToString();
                return usuario;
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

        private static SqlCommand ConsultaUsuarioPorId(SqlConnection connection, int idUsuario)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = $"select * from Usuario where idUsuario = {idUsuario}";
            connection.Open();
            return sqlCommand;
        }
    }
}
