using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Linq;
using Xunit;

namespace XUnitReviewPeli
{
    public class UnitTestReview
    {
        [Fact]
        public void TestObtenerReviews()
        {
            //Arrange
            bool estaVacio = false;

            //Act
            var Resultado = ReviewAzure.ObtenerReviews();

            estaVacio = !Resultado.Any();

            //Assert
            Assert.False(estaVacio);

        }

        [Fact]
        public void TestObtenerReviewPorId()
        {
            //Arrange
            int idProbar = 1;
            Review reviewRetornada;

            //Act
            reviewRetornada = ReviewAzure.ObtenerReviewPorId(idProbar);

            //Assert
            Assert.NotNull(reviewRetornada);
        }

        [Fact]
        public void TestAgregarReview()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Review review = new Review
            {
                descripcion = "aaa",
                idPelicula = 2,
                idReview = 4
            };

            //Act
            resultadoObtenido = ReviewAzure.AgregarReview(review);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestEliminarReview()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            //Review review = new Review
            //{
            //    descripcion = "Excelente Pelicula",
            //    idPelicula = 2,
            //    idReview = 5
            //};

            ////Act
            //ReviewAzure.AgregarReview(review);
            resultadoObtenido = ReviewAzure.EliminarReview(4);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestActualizarReview()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Review review = new Review
            {
                descripcion = "uwu",
                idPelicula = 2,
                idReview = 4
            };

            //Act
            resultadoObtenido = ReviewAzure.ActualizarReview(review);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}