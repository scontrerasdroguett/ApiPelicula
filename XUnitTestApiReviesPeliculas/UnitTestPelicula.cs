using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Linq;
using Xunit;
using System.Collections;

namespace XUnitTestApiReviesPeliculas
{
    public class UnitTestPelicula
    {
        [Fact]
        public void TestObtenerPeliculas()
        {
            //Arrange
            bool estaVacio = false;

            //Act
            var Resultado = PeliculaAzure.ObtenerPelicula();

            estaVacio = !Resultado.Any();

            //Assert
            Assert.False(estaVacio);

        }

        [Fact]
        public void TestObtenerPeliculaPorId()
        {
            //Arrange
            int idPelicula = 1;
            Pelicula peliculaRetornado;

            //Act
            peliculaRetornado = PeliculaAzure.ObtenerPeliculaPorId(idPelicula);

            //Assert
            Assert.NotNull(peliculaRetornado);
        }

        [Fact]
        public void TestAgregarPelicula()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Pelicula pelicula = new Pelicula
            {
             idioma = "Ingles",
             categoria = "Fantasia",
             director = "Chris Columbus",
             actor= "Daniel Radcliffe",
             sinopsis = "uwu",
             titulo = "Harry Potter y la piedra filosofal",
             idUsuario = 2,
             idPelicula = 3
            };

            //Act
            resultadoObtenido = PeliculaAzure.AgregarPelicula(pelicula);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestEliminarPelicula()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Pelicula pelicula = new Pelicula
            {
                idioma = "Ingles",
                categoria = "Fantasia",
                director = "Chris Columbus",
                actor = "Daniel Radcliffe",
                sinopsis = "Harry Potter y los estudiantes de segundo año investigan una malévola amenaza para sus compañeros de clases de Hogwarts.",
                titulo = "Harry Potter y la camara secreta",
                idUsuario = 2,
                idPelicula = 4
            };

            //Act
            PeliculaAzure.AgregarPelicula(pelicula);

            resultadoObtenido = PeliculaAzure.EliminarPelicula(4);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
        [Fact]
        public void TestActualizarPelicula()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Pelicula peli = new Pelicula
            {
                idioma = "Ingles",
                categoria = "Fantasia",
                director = "Chris Columbus",
                actor = "Daniel Radcliffe",
                sinopsis = "uwu",
                titulo = "Harry Potter and the philosopher's stone",
                idUsuario = 2,
                idPelicula = 3
            };

            //Act
            resultadoObtenido = PeliculaAzure.ActualizarPelicula(peli);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

    }
}
