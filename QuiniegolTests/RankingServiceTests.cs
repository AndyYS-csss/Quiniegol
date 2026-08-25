using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiniegol.Services;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolTests
{
    [TestClass]
    public class RankingServiceTests
    {
        [TestMethod]
        public void ObtenerRankingGlobal_DebeOrdenarPorPuntosDescendente()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 20),
                new Usuario("Ana", "Brasil", 50),
                new Usuario("Pedro", "México", 30)
            };

            var resultado =
                service.ObtenerRankingGlobal(usuarios);

            Assert.AreEqual("Ana", resultado[0].Nombre);
            Assert.AreEqual("Pedro", resultado[1].Nombre);
            Assert.AreEqual("Carlos", resultado[2].Nombre);
        }

        [TestMethod]
        public void ObtenerRankingGlobal_NoDebeModificarListaOriginal()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 20),
                new Usuario("Ana", "Brasil", 50),
                new Usuario("Pedro", "México", 30)
            };

            service.ObtenerRankingGlobal(usuarios);

            Assert.AreEqual("Carlos", usuarios[0].Nombre);
            Assert.AreEqual("Ana", usuarios[1].Nombre);
            Assert.AreEqual("Pedro", usuarios[2].Nombre);
        }

        [TestMethod]
        public void ObtenerRankingGlobal_DebeRetornarListaVaciaSiUsuariosEsNull()
        {
            var service = new RankingService();

            var resultado =
                service.ObtenerRankingGlobal(null!);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void ObtenerRankingGlobal_DebeRetornarListaVaciaSiNoHayUsuarios()
        {
            var service = new RankingService();

            var usuarios =
                new List<Usuario>();

            var resultado =
                service.ObtenerRankingGlobal(usuarios);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void ObtenerRankingGlobal_DebeOrdenarEmpatesPorNombre()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Pedro", "México", 20),
                new Usuario("Ana", "Brasil", 20),
                new Usuario("Carlos", "Costa Rica", 30)
            };

            var resultado =
                service.ObtenerRankingGlobal(usuarios);

            Assert.AreEqual("Carlos", resultado[0].Nombre);
            Assert.AreEqual("Ana", resultado[1].Nombre);
            Assert.AreEqual("Pedro", resultado[2].Nombre);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeOrdenarIntegrantes()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(
                new Usuario("Pedro", "México", 20));

            quiniela.Integrantes.Add(
                new Usuario("Ana", "Brasil", 50));

            quiniela.Integrantes.Add(
                new Usuario("Carlos", "Costa Rica", 30));

            var resultado =
                service.ObtenerRankingPrivado(quiniela);

            Assert.AreEqual("Ana", resultado[0].Nombre);
            Assert.AreEqual("Carlos", resultado[1].Nombre);
            Assert.AreEqual("Pedro", resultado[2].Nombre);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeRetornarListaVaciaSiQuinielaEsPublica()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Quiniela Pública", false);

            quiniela.Integrantes.Add(
                new Usuario("Ana", "Brasil", 50));

            var resultado =
                service.ObtenerRankingPrivado(quiniela);

            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeRetornarListaVaciaSiQuinielaEsNull()
        {
            var service = new RankingService();

            var resultado =
                service.ObtenerRankingPrivado(null!);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeRetornarListaVaciaSiNoHayIntegrantes()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            var resultado =
                service.ObtenerRankingPrivado(quiniela);

            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeOrdenarEmpatesPorNombre()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(
                new Usuario("Pedro", "México", 20));

            quiniela.Integrantes.Add(
                new Usuario("Ana", "Brasil", 20));

            var resultado =
                service.ObtenerRankingPrivado(quiniela);

            Assert.AreEqual("Ana", resultado[0].Nombre);
            Assert.AreEqual("Pedro", resultado[1].Nombre);
        }

        [TestMethod]
        public void ObtenerRankingGlobal_DebeConservarCantidadDeUsuarios()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 10),
                new Usuario("Ana", "Brasil", 20),
                new Usuario("Pedro", "México", 30)
            };

            var resultado =
                service.ObtenerRankingGlobal(usuarios);

            Assert.AreEqual(
                usuarios.Count,
                resultado.Count);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeConservarCantidadDeIntegrantes()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(
                new Usuario("Carlos", "Costa Rica", 10));

            quiniela.Integrantes.Add(
                new Usuario("Ana", "Brasil", 20));

            quiniela.Integrantes.Add(
                new Usuario("Pedro", "México", 30));

            var resultado =
                service.ObtenerRankingPrivado(quiniela);

            Assert.AreEqual(
                quiniela.Integrantes.Count,
                resultado.Count);
        }

        // ============================================================
        // POSICIONES
        // ============================================================

        [TestMethod]
        public void ObtenerPosicionGlobal_DebeRetornarPrimeraPosicion()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 20),
                new Usuario("Ana", "Brasil", 50),
                new Usuario("Pedro", "México", 30)
            };

            var posicion =
                service.ObtenerPosicionGlobal(
                    usuarios,
                    "Ana");

            Assert.AreEqual(1, posicion);
        }

        [TestMethod]
        public void ObtenerPosicionGlobal_DebeRetornarPosicionCorrecta()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 20),
                new Usuario("Ana", "Brasil", 50),
                new Usuario("Pedro", "México", 30)
            };

            var posicion =
                service.ObtenerPosicionGlobal(
                    usuarios,
                    "Pedro");

            Assert.AreEqual(2, posicion);
        }

        [TestMethod]
        public void ObtenerPosicionGlobal_DebeRetornarCeroSiUsuarioNoExiste()
        {
            var service = new RankingService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 20),
                new Usuario("Ana", "Brasil", 50)
            };

            var posicion =
                service.ObtenerPosicionGlobal(
                    usuarios,
                    "UsuarioInexistente");

            Assert.AreEqual(0, posicion);
        }

        [TestMethod]
        public void ObtenerPosicionPrivada_DebeRetornarPosicionCorrecta()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(
                new Usuario("Pedro", "México", 20));

            quiniela.Integrantes.Add(
                new Usuario("Ana", "Brasil", 50));

            quiniela.Integrantes.Add(
                new Usuario("Carlos", "Costa Rica", 30));

            var posicion =
                service.ObtenerPosicionPrivada(
                    quiniela,
                    "Carlos");

            Assert.AreEqual(2, posicion);
        }

        [TestMethod]
        public void ObtenerPosicionPrivada_DebeRetornarCeroSiUsuarioNoExiste()
        {
            var service = new RankingService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(
                new Usuario("Ana", "Brasil", 50));

            var posicion =
                service.ObtenerPosicionPrivada(
                    quiniela,
                    "UsuarioInexistente");

            Assert.AreEqual(0, posicion);
        }

        // ============================================================
        // ACTUALIZACIÓN AUTOMÁTICA
        // ============================================================

        [TestMethod]
        public void ObtenerRankingGlobal_DebeActualizarPosicionCuandoCambianLosPuntos()
        {
            var service = new RankingService();

            var carlos =
                new Usuario("Carlos", "Costa Rica", 20);

            var ana =
                new Usuario("Ana", "Brasil", 50);

            var usuarios = new List<Usuario>
            {
                carlos,
                ana
            };

            var posicionInicial =
                service.ObtenerPosicionGlobal(
                    usuarios,
                    "Carlos");

            Assert.AreEqual(2, posicionInicial);

            carlos.Puntos = 60;

            var posicionActualizada =
                service.ObtenerPosicionGlobal(
                    usuarios,
                    "Carlos");

            Assert.AreEqual(1, posicionActualizada);
        }

        [TestMethod]
        public void ObtenerRankingPrivado_DebeActualizarPosicionCuandoCambianLosPuntos()
        {
            var service = new RankingService();

            var ana =
                new Usuario("Ana", "Brasil", 50);

            var pedro =
                new Usuario("Pedro", "México", 20);

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(ana);
            quiniela.Integrantes.Add(pedro);

            var posicionInicial =
                service.ObtenerPosicionPrivada(
                    quiniela,
                    "Pedro");

            Assert.AreEqual(2, posicionInicial);

            pedro.Puntos = 60;

            var posicionActualizada =
                service.ObtenerPosicionPrivada(
                    quiniela,
                    "Pedro");

            Assert.AreEqual(1, posicionActualizada);
        }
    }
}