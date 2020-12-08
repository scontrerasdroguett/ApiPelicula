using ReviewPeliculas.Azure;
using ReviewPeliculas.Models;
using System;
using System.Linq;
using Xunit;
using System.Collections;

namespace XUnitTestApiReviesPeliculas
{
    public class UnitTestRol
        {
        [Fact]
        public void TestObtenerRoles()
        {
            //Arrange
            bool estaVacio = false;

            //Act
            var Resultado = RolAzure.ObtenerRol();

            estaVacio = !Resultado.Any();

            //Assert
            Assert.False(estaVacio);

        }

    }
}
