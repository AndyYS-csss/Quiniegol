using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiniegol.Services;
using QuiniegolModels;
using System;

namespace QuiniegolTests
{
    [TestClass]
    public class PuntuacionServiceTest
    {
        // ============================================================
        // PRUEBA 1
        // Marcador exacto = 5 puntos
        // ============================================================

        [TestMethod]
        public void CalcularPuntos_DebeDar5PorMarcadorExacto()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    2,
                    1);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 1;

            var puntos =
                service.CalcularPuntos(
                    pronostico,
                    partido);

            Assert.AreEqual(
                5,
                puntos);
        }

        // ============================================================
        // PRUEBA 2
        // Acierta ganador = 2 puntos
        // ============================================================

        [TestMethod]
        public void CalcularPuntos_DebeDar2PorAcertarGanador()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    3,
                    1);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 0;

            var puntos =
                service.CalcularPuntos(
                    pronostico,
                    partido);

            Assert.AreEqual(
                2,
                puntos);
        }

        // ============================================================
        // PRUEBA 3
        // Acierta empate = 2 puntos
        // ============================================================

        [TestMethod]
        public void CalcularPuntos_DebeDar2PorAcertarEmpate()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    1,
                    1);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 2;

            var puntos =
                service.CalcularPuntos(
                    pronostico,
                    partido);

            Assert.AreEqual(
                2,
                puntos);
        }

        // ============================================================
        // PRUEBA 4
        // Ningún acierto = 0 puntos
        // ============================================================

        [TestMethod]
        public void CalcularPuntos_DebeDar0SiNoAcertado()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    0,
                    2);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 1;

            var puntos =
                service.CalcularPuntos(
                    pronostico,
                    partido);

            Assert.AreEqual(
                0,
                puntos);
        }

        // ============================================================
        // PRUEBA 5
        // Pronóstico null = 0 puntos
        // ============================================================

        [TestMethod]
        public void CalcularPuntos_DebeDar0SiPronosticoEsNull()
        {
            var service =
                new PuntuacionService();

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 1;

            var puntos =
                service.CalcularPuntos(
                    null,
                    partido);

            Assert.AreEqual(
                0,
                puntos);
        }

        // ============================================================
        // PRUEBA 6
        // Partido null = 0 puntos
        // ============================================================

        [TestMethod]
        public void CalcularPuntos_DebeDar0SiPartidoEsNull()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    2,
                    1);

            var puntos =
                service.CalcularPuntos(
                    pronostico,
                    null);

            Assert.AreEqual(
                0,
                puntos);
        }

        // ============================================================
        // PRUEBA 7
        // Actualiza Pronostico.Puntos = 5
        // ============================================================

        [TestMethod]
        public void ActualizarPuntosPronostico_DebeActualizarPuntos()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    2,
                    1);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 1;
            partido.Finalizado = true;

            var resultado =
                service.ActualizarPuntosPronostico(
                    pronostico,
                    partido);

            Assert.IsTrue(resultado);

            Assert.AreEqual(
                5,
                pronostico.Puntos);
        }

        // ============================================================
        // PRUEBA 8
        // No actualiza si Pronostico es null
        // ============================================================

        [TestMethod]
        public void ActualizarPuntosPronostico_DebeRetornarFalseSiPronosticoEsNull()
        {
            var service =
                new PuntuacionService();

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),
                    new Seleccion(
                        "Brasil",
                        "BRA"),
                    new DateTime(
                        2026,
                        12,
                        1,
                        20,
                        0,
                        0));

            partido.GolesLocal = 2;
            partido.GolesVisitante = 1;

            var resultado =
                service.ActualizarPuntosPronostico(
                    null,
                    partido);

            Assert.IsFalse(resultado);
        }

        // ============================================================
        // PRUEBA 9
        // No actualiza si Partido es null
        // ============================================================

        [TestMethod]
        public void ActualizarPuntosPronostico_DebeRetornarFalseSiPartidoEsNull()
        {
            var service =
                new PuntuacionService();

            var pronostico =
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    2,
                    1);

            var resultado =
                service.ActualizarPuntosPronostico(
                    pronostico,
                    null);

            Assert.IsFalse(resultado);
        }
    }
}