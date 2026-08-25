using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuiniegolController;
using QuiniegolModels;
using System;
using System.IO;

namespace QuiniegolTests
{
    [TestClass]
    public class PronosticoControllerTest
    {
        private string archivoPrueba = string.Empty;

        [TestInitialize]
        public void Inicializar()
        {
            archivoPrueba = Path.Combine(
                Path.GetTempPath(),
                "PronosticosTest_" +
                Guid.NewGuid().ToString() +
                ".csv");

            File.WriteAllText(
                archivoPrueba,
                "NombreUsuario,Local,Visitante,GolesLocal,GolesVisitante,Puntos" +
                Environment.NewLine +
                "Carlos,Costa Rica,Brasil,2,1,0");
        }

        [TestCleanup]
        public void Limpiar()
        {
            if (File.Exists(archivoPrueba))
            {
                File.Delete(archivoPrueba);
            }
        }

        [TestMethod]
        public void Load_DebeCargarLosPronosticos()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            var pronosticos =
                controller.Load(archivoPrueba);

            Assert.IsNotNull(pronosticos);
            Assert.AreEqual(1, pronosticos.Count);
        }

        [TestMethod]
        public void FindPronostico_DebeEncontrarPronosticoExistente()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            controller.Load(archivoPrueba);

            var pronostico =
                controller.FindPronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil");

            Assert.IsNotNull(pronostico);

            Assert.AreEqual(
                "Carlos",
                pronostico.NombreUsuario);

            Assert.AreEqual(
                "Costa Rica",
                pronostico.Local);

            Assert.AreEqual(
                "Brasil",
                pronostico.Visitante);

            Assert.AreEqual(
                2,
                pronostico.GolesLocal);

            Assert.AreEqual(
                1,
                pronostico.GolesVisitante);

            Assert.AreEqual(
                0,
                pronostico.Puntos);
        }

        [TestMethod]
        public void FindPronostico_DebeRetornarNullSiNoExiste()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            controller.Load(archivoPrueba);

            var pronostico =
                controller.FindPronostico(
                    "UsuarioInexistente",
                    "Costa Rica",
                    "Brasil");

            Assert.IsNull(pronostico);
        }

        [TestMethod]
        public void RegisterPronostico_DebeRegistrarPronostico()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            controller.Load(archivoPrueba);

            var partido = new Partido(
                new Seleccion("Costa Rica", "A"),
                new Seleccion("Brasil", "A"),
                new DateTime(2026, 12, 1, 20, 0, 0));

            var fechaSistema =
                new DateTime(2026, 12, 1, 18, 0, 0);

            var resultado =
                controller.RegisterPronostico(
                    archivoPrueba,
                    "Ana",
                    partido,
                    2,
                    0,
                    fechaSistema);

            Assert.IsTrue(resultado);

            var pronostico =
                controller.FindPronostico(
                    "Ana",
                    "Costa Rica",
                    "Brasil");

            Assert.IsNotNull(pronostico);

            Assert.AreEqual(
                2,
                pronostico.GolesLocal);

            Assert.AreEqual(
                0,
                pronostico.GolesVisitante);

            Assert.AreEqual(
                0,
                pronostico.Puntos);
        }

        [TestMethod]
        public void RegisterPronostico_NoDebeRegistrarSiPartidoYaInicio()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            controller.Load(archivoPrueba);

            var partido = new Partido(
                new Seleccion("Costa Rica", "A"),
                new Seleccion("Brasil", "A"),
                new DateTime(2026, 12, 1, 20, 0, 0));

            var fechaSistema =
                new DateTime(2026, 12, 1, 20, 1, 0);

            var resultado =
                controller.RegisterPronostico(
                    archivoPrueba,
                    "Ana",
                    partido,
                    2,
                    0,
                    fechaSistema);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void RegisterPronostico_NoDebeRegistrarPronosticoDuplicado()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            controller.Load(archivoPrueba);

            var partido = new Partido(
                new Seleccion("Costa Rica", "A"),
                new Seleccion("Brasil", "A"),
                new DateTime(2026, 12, 1, 20, 0, 0));

            var fechaSistema =
                new DateTime(2026, 12, 1, 18, 0, 0);

            var primerResultado =
                controller.RegisterPronostico(
                    archivoPrueba,
                    "Ana",
                    partido,
                    2,
                    0,
                    fechaSistema);

            Assert.IsTrue(primerResultado);

            var segundoResultado =
                controller.RegisterPronostico(
                    archivoPrueba,
                    "Ana",
                    partido,
                    3,
                    1,
                    fechaSistema);

            Assert.IsFalse(segundoResultado);
        }

        [TestMethod]
        public void RegisterPronostico_NoDebeAceptarGolesNegativos()
        {
            var dataHandler =
                new FileHandler<Pronostico>();

            var controller =
                new PronosticoController(dataHandler);

            controller.Load(archivoPrueba);

            var partido = new Partido(
                new Seleccion("Costa Rica", "A"),
                new Seleccion("Brasil", "A"),
                new DateTime(2026, 12, 1, 20, 0, 0));

            var fechaSistema =
                new DateTime(2026, 12, 1, 18, 0, 0);

            var resultado =
                controller.RegisterPronostico(
                    archivoPrueba,
                    "Ana",
                    partido,
                    -1,
                    2,
                    fechaSistema);

            Assert.IsFalse(resultado);
        }
    }
}