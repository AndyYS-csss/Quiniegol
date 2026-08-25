using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuiniegolController;
using QuiniegolModels;
using System;
using System.IO;

namespace QuiniegolTests
{
    [TestClass]
    public class PartidoControllerTest
    {
        private string archivoPrueba = string.Empty;

        [TestInitialize]
        public void Inicializar()
        {
            archivoPrueba = Path.Combine(
                Path.GetTempPath(),
                "PartidosTest_" +
                Guid.NewGuid().ToString() +
                ".csv");

            File.WriteAllText(
                archivoPrueba,
                "Local,LocalCodigo,Visitante,VisitanteCodigo,Fecha" +
                Environment.NewLine +
                "Costa Rica,CRC,Mexico,MEX,2026-08-25 10:00:00" +
                Environment.NewLine +
                "Brasil,BRA,Argentina,ARG,2026-08-25 12:00:00");
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
        public void Load_DebeCargarLosPartidos()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            var partidos =
                controller.Load(archivoPrueba);

            Assert.IsNotNull(partidos);
            Assert.AreEqual(2, partidos.Count);
        }

        [TestMethod]
        public void Load_DebeCargarCorrectamenteLosEquipos()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            var partidos =
                controller.Load(archivoPrueba);

            Assert.AreEqual(2, partidos.Count);

            var partido = partidos[0];

            Assert.AreEqual(
                "Costa Rica",
                partido.Local.Nombre);

            Assert.AreEqual(
                "Mexico",
                partido.Visitante.Nombre);
        }

        [TestMethod]
        public void FindMatch_DebeEncontrarPartidoExistente()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            controller.Load(archivoPrueba);

            var partido =
                controller.FindMatch(
                    "Costa Rica",
                    "Mexico");

            Assert.IsNotNull(partido);

            Assert.AreEqual(
                "Costa Rica",
                partido.Local.Nombre);

            Assert.AreEqual(
                "Mexico",
                partido.Visitante.Nombre);
        }

        [TestMethod]
        public void FindMatch_DebeRetornarNullSiNoExiste()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            controller.Load(archivoPrueba);

            var partido =
                controller.FindMatch(
                    "Costa Rica",
                    "Brasil");

            Assert.IsNull(partido);
        }

        [TestMethod]
        public void UpdateResult_DebeActualizarElResultado()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.UpdateResult(
                    "Costa Rica",
                    "Mexico",
                    2,
                    1);

            Assert.IsTrue(resultado);

            var partido =
                controller.FindMatch(
                    "Costa Rica",
                    "Mexico");

            Assert.IsNotNull(partido);

            Assert.AreEqual(2, partido.GolesLocal);
            Assert.AreEqual(1, partido.GolesVisitante);
            Assert.IsTrue(partido.Finalizado);
        }

        [TestMethod]
        public void UpdateResult_DebeRechazarGolesNegativos()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.UpdateResult(
                    "Costa Rica",
                    "Mexico",
                    -1,
                    2);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void UpdateResult_DebeRetornarFalseSiElPartidoNoExiste()
        {
            var dataHandler = new FileHandler<Partido>();

            var controller =
                new PartidoController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.UpdateResult(
                    "Costa Rica",
                    "Brasil",
                    2,
                    1);

            Assert.IsFalse(resultado);
        }
    }
}