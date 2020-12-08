using ReviewPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewPeliculas.Azure
{
    public class ReviewAzure
    {
        static string connectionString = "Server=DESKTOP-UJCISGT;Database=ApiReviewPelicula;Trusted_Connection=True;";

        private static List<Review> reviews;

        //Obtener todas las reviews
        public static List<Review> ObtenerReviews()
        {
            var dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "select * from Review";

                connection.Open();
                var DataAdapter = new SqlDataAdapter(sqlCommand);

                DataAdapter.Fill(dataTable);

                reviews = new List<Review>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Review review = new Review();
                    review.idReview = int.Parse(dataTable.Rows[i]["idReview"].ToString());
                    review.idPelicula = int.Parse(dataTable.Rows[i]["idPelicula"].ToString());
                    review.descripcion = dataTable.Rows[i]["descripcion"].ToString();

                    reviews.Add(review);
                }
                return reviews;
            }
        }

        //Obtener review por ID
        public static Review ObtenerReviewPorId(int idReview)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //1.- Creación y apertura de conexión
                var comando = ConsultaReviewPorId(connection, idReview);

                //2.- Llenado de datatable y conversión
                var dataTable = LlenarDataTable(comando);

                //3.- Crear y retornar objeto review
                return CreacionReview(dataTable);
            }
        }

        //Agregar review 
        public static int AgregarReview(Review review)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "insert into Review values(@idReview,@idPelicula, @descripcion)";
                sqlCommand.Parameters.AddWithValue("@idReview", review.idReview);
                sqlCommand.Parameters.AddWithValue("@idPelicula", review.idPelicula);
                sqlCommand.Parameters.AddWithValue("@descripcion", review.descripcion);
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
        public static int EliminarReview(int idReview)
        {
            int delete = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "delete from Review where idReview = @idReview";
                sqlCommand.Parameters.AddWithValue("@idReview", idReview);
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
        public static int ActualizarReview(Review review)
        {
            int insert = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "update Review set descripcion = @descripcion where idReview = @idReview";
                sqlCommand.Parameters.AddWithValue("@descripcion", review.descripcion);
                sqlCommand.Parameters.AddWithValue("@idReview", review.idReview);
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

        private static Review CreacionReview(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Review review = new Review();
                review.idReview = int.Parse(dataTable.Rows[0]["idReview"].ToString());
                review.idPelicula = int.Parse(dataTable.Rows[0]["idPelicula"].ToString());
                review.descripcion = dataTable.Rows[0]["descripcion"].ToString();
                return review;
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

        private static SqlCommand ConsultaReviewPorId(SqlConnection connection, int idReview)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = $"select * from Review where idReview = {idReview}";
            connection.Open();
            return sqlCommand;
        }
    }
}

