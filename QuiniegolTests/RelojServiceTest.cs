using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiniegol.Services;
using QuiniegolModels;
using System;

namespace QuiniegolTests
{
    [TestClass]
    public class RelojServiceTest
    {
        [TestMethod]
        public void ObtenerFechaHoraActual_DebeRetornarFechaActual()
        {
            var relojService = new RelojService();

            var fechaAntes =
                Reloj.Instance.CurrentDateTime;

            var fechaActual =
                relojService.ObtenerFechaHoraActual();

            var fechaDespues =
                Reloj.Instance.CurrentDateTime;

            Assert.IsTrue(
                fechaActual >= fechaAntes &&
                fechaActual <= fechaDespues);
        }

        [TestMethod]
        public void AvanzarTiempo_DebeAvanzarElReloj()
        {
            var relojService = new RelojService();

            var fechaInicial =
                relojService.ObtenerFechaHoraActual();

            var cantidad =
                TimeSpan.FromMinutes(10);

            relojService.AvanzarTiempo(cantidad);

            var fechaFinal =
                relojService.ObtenerFechaHoraActual();

            Assert.AreEqual(
                fechaInicial.Add(cantidad),
                fechaFinal);
        }

        [TestMethod]
        public void AvanzarTiempo_NoDebeAvanzarConCantidadNegativa()
        {
            var relojService = new RelojService();

            var fechaInicial =
                relojService.ObtenerFechaHoraActual();

            relojService.AvanzarTiempo(
                TimeSpan.FromMinutes(-10));

            var fechaFinal =
                relojService.ObtenerFechaHoraActual();

            Assert.AreEqual(
                fechaInicial,
                fechaFinal);
        }

        [TestMethod]
        public void AvanzarTiempo_NoDebeAvanzarConCantidadCero()
        {
            var relojService = new RelojService();

            var fechaInicial =
                relojService.ObtenerFechaHoraActual();

            relojService.AvanzarTiempo(
                TimeSpan.Zero);

            var fechaFinal =
                relojService.ObtenerFechaHoraActual();

            Assert.AreEqual(
                fechaInicial,
                fechaFinal);
        }

        [TestMethod]
        public void AceptaPronosticos_DebeRetornarFalseSiPartidoEsNull()
        {
            var relojService = new RelojService();

            var resultado =
                relojService.AceptaPronosticos(null);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EstaEnCurso_DebeRetornarFalseSiPartidoEsNull()
        {
            var relojService = new RelojService();

            var resultado =
                relojService.EstaEnCurso(null);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void AceptaPronosticos_DebeRetornarTrueAntesDelPartido()
        {
            var relojService = new RelojService();

            var fechaActual =
                relojService.ObtenerFechaHoraActual();

            var partido = new Partido(
                new Seleccion("Costa Rica", "A"),
                new Seleccion("Brasil", "A"),
                fechaActual.AddHours(1));

            var resultado =
                relojService.AceptaPronosticos(partido);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EstaEnCurso_DebeRetornarTrueCuandoElPartidoInicia()
        {
            var relojService = new RelojService();

            var fechaActual =
                relojService.ObtenerFechaHoraActual();

            var partido = new Partido(
                new Seleccion("Costa Rica", "A"),
                new Seleccion("Brasil", "A"),
                fechaActual.AddMinutes(-1));

            var resultado =
                relojService.EstaEnCurso(partido);

            Assert.IsTrue(resultado);
        }
    }
}