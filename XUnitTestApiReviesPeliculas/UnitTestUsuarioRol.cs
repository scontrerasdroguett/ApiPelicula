using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Linq;
using Xunit;
using System.Collections;

namespace XUnitTestApiReviesPeliculas
{
    public class UnitTestUsuarioRol
    {
        [Fact]
        public void TestObtenerUsuarioRol()
        {
            //Arrange
            bool estaVacio = false;

            //Act
            var Resultado = UsuarioRolAzure.ObtenerUsuarioRol();

            estaVacio = !Resultado.Any();

            //Assert
            Assert.False(estaVacio);

        }

        [Fact]
        public void TestAgregarUsuarioRol()
        {
            //Arrange
            int resultadoObtenido;
            int resultadoEsperado = 1;
            UsuarioRol user = new UsuarioRol
            {
                idUsuarioRol = 4,
                idRol = 3,
                idUsuario = 4,

            };

            //Act
            resultadoObtenido = UsuarioRolAzure.AgregarUsuarioRol(user);

            //Assert
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }

}
