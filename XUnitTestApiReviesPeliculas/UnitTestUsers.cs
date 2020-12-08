using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Linq;
using Xunit;
using System.Collections;

namespace XUnitTestApiReviesPeliculas
{
    public class UnitTestUsers
    {
        [Fact]
        public void TestObtenerUsuarios()
        {
            //Arrange
            bool estaVacio = false;

            //Act
            var Resultado = UsuarioAzure.ObtenerUsuario();

            estaVacio = !Resultado.Any();

            //Assert
            Assert.False(estaVacio);

        }

        [Fact]
        public void TestObtenerReviewPorId()
        {
            //Arrange
            int idProbar = 1;
            Usuario usuarioRetornado;

            //Act
            usuarioRetornado = UsuarioAzure.ObtenerUsuarioPorId(idProbar);

            //Assert
            Assert.NotNull(usuarioRetornado);
        }

        [Fact]
        public void TestAgregarUsuario()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Usuario user = new Usuario
            {
                email = "jorge@gmail.com",
                genero = "Masculino",
                edad = 28,
                apellidos="Perez",
                nombres="Jorge",
                idUsuario = 4
            };

            //Act
            resultadoObtenido = UsuarioAzure.AgregarUsuario(user);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void TestEliminarReview()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            //Usuario user = new Usuario
            //{
            //    email = "jorge@gmail.com",
            //    genero = "Masculino",
            //    edad = 28,
            //    apellidos = "Perez",
            //    nombres = "Jorge",
            //    idUsuario = 4
            //};

            ////Act
            //UsuarioAzure.AgregarUsuario(user);

            resultadoObtenido = UsuarioAzure.EliminarUsuario(4);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
        [Fact]
        public void TestActualizarUsuario()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            Usuario user = new Usuario
            {
                email = "perez@gmail.com",
                genero = "Femenino",
                edad = 21,
                apellidos = "Perez ",
                nombres = "Jorge Manuel ",
                idUsuario = 4
            };

            //Act
            resultadoObtenido = UsuarioAzure.ActualizarUsuario(user);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

    }
}
