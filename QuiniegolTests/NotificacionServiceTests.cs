using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiniegol.Services;
using QuiniegolModels;
using System;
using System.Collections.Generic;

namespace QuiniegolTests
{
    [TestClass]
    public class NotificacionServiceTests
    {
        [TestMethod]
        public void AgregarNotificacion_DebeAgregarAlTimeline()
        {
            var service = new NotificacionService();

            var quiniela =
                new Quiniela(
                    "Quiniela Prueba",
                    true);

            bool resultado =
                service.AgregarNotificacion(
                    quiniela,
                    "Nuevo líder: Carlos.",
                    DateTime.Now);

            Assert.IsTrue(resultado);

            Assert.AreEqual(
                1,
                quiniela.Notificaciones.Count);

            Assert.AreEqual(
                "Nuevo líder: Carlos.",
                quiniela.Notificaciones[0].Mensaje);
        }

        [TestMethod]
        public void AgregarNotificacion_QuinielaNula_DebeRetornarFalse()
        {
            var service = new NotificacionService();

            bool resultado =
                service.AgregarNotificacion(
                    null,
                    "Nuevo líder.",
                    DateTime.Now);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void AgregarNotificacion_MensajeVacio_DebeRetornarFalse()
        {
            var service = new NotificacionService();

            var quiniela =
                new Quiniela(
                    "Quiniela Prueba",
                    true);

            bool resultado =
                service.AgregarNotificacion(
                    quiniela,
                    "",
                    DateTime.Now);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ObtenerTimeline_DebeRetornarNotificacionesDeLaQuiniela()
        {
            var service = new NotificacionService();

            var quiniela =
                new Quiniela(
                    "Quiniela Prueba",
                    true);

            service.AgregarNotificacion(
                quiniela,
                "Jugador acertó el marcador.",
                DateTime.Now);

            service.AgregarNotificacion(
                quiniela,
                "Nuevo líder: Ana.",
                DateTime.Now);

            var timeline =
                service.ObtenerTimeline(
                    quiniela);

            Assert.AreEqual(
                2,
                timeline.Count);
        }

        [TestMethod]
        public void NotificarNuevoLider_DebeCrearNotificacion()
        {
            var service = new NotificacionService();

            var quiniela =
                new Quiniela(
                    "Quiniela Prueba",
                    true);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            bool resultado =
                service.NotificarNuevoLider(
                    quiniela,
                    usuario,
                    DateTime.Now);

            Assert.IsTrue(resultado);

            Assert.AreEqual(
                1,
                quiniela.Notificaciones.Count);

            StringAssert.Contains(
                quiniela.Notificaciones[0].Mensaje,
                "Carlos");
        }

        [TestMethod]
        public void NotificarRacha_DebeCrearNotificacion()
        {
            var service = new NotificacionService();

            var quiniela =
                new Quiniela(
                    "Quiniela Prueba",
                    true);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            bool resultado =
                service.NotificarRacha(
                    quiniela,
                    usuario,
                    11,
                    DateTime.Now);

            Assert.IsTrue(resultado);

            StringAssert.Contains(
                quiniela.Notificaciones[0].Mensaje,
                "11");
        }

        // =========================================================
        // PRUEBAS DEL PROYECTO 2
        // NOTIFICACIONES DE PARTIDOS DENTRO DE 24 HORAS
        // =========================================================

        [TestMethod]
        public void ObtenerNotificacionesPartidosPendientes_DebeNotificarPartidoDentroDe24Horas()
        {
            var service = new NotificacionService();

            var fechaSistema =
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "A"),
                    new Seleccion(
                        "Brasil",
                        "A"),
                    fechaSistema.AddHours(5));

            var partidos =
                new List<Partido>
                {
                    partido
                };

            var resultado =
                service.ObtenerNotificacionesPartidosPendientes(
                    usuario,
                    partidos,
                    fechaSistema);

            Assert.AreEqual(
                1,
                resultado.Count);

            StringAssert.Contains(
                resultado[0].Mensaje,
                "Costa Rica");

            StringAssert.Contains(
                resultado[0].Mensaje,
                "Brasil");
        }

        [TestMethod]
        public void ObtenerNotificacionesPartidosPendientes_NoDebeNotificarSiYaExistePronostico()
        {
            var service = new NotificacionService();

            var fechaSistema =
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "A"),
                    new Seleccion(
                        "Brasil",
                        "A"),
                    fechaSistema.AddHours(5));

            usuario.Pronosticos.Add(
                new Pronostico(
                    "Carlos",
                    "Costa Rica",
                    "Brasil",
                    2,
                    1));

            var partidos =
                new List<Partido>
                {
                    partido
                };

            var resultado =
                service.ObtenerNotificacionesPartidosPendientes(
                    usuario,
                    partidos,
                    fechaSistema);

            Assert.AreEqual(
                0,
                resultado.Count);
        }

        [TestMethod]
        public void ObtenerNotificacionesPartidosPendientes_NoDebeNotificarPartidoFueraDe24Horas()
        {
            var service = new NotificacionService();

            var fechaSistema =
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "A"),
                    new Seleccion(
                        "Brasil",
                        "A"),
                    fechaSistema.AddHours(25));

            var partidos =
                new List<Partido>
                {
                    partido
                };

            var resultado =
                service.ObtenerNotificacionesPartidosPendientes(
                    usuario,
                    partidos,
                    fechaSistema);

            Assert.AreEqual(
                0,
                resultado.Count);
        }

        [TestMethod]
        public void ObtenerNotificacionesPartidosPendientes_NoDebeNotificarPartidoYaIniciado()
        {
            var service = new NotificacionService();

            var fechaSistema =
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "A"),
                    new Seleccion(
                        "Brasil",
                        "A"),
                    fechaSistema.AddMinutes(-30));

            var partidos =
                new List<Partido>
                {
                    partido
                };

            var resultado =
                service.ObtenerNotificacionesPartidosPendientes(
                    usuario,
                    partidos,
                    fechaSistema);

            Assert.AreEqual(
                0,
                resultado.Count);
        }

        [TestMethod]
        public void ObtenerNotificacionesPartidosPendientes_UsuarioNulo_DebeRetornarListaVacia()
        {
            var service = new NotificacionService();

            var fechaSistema =
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0);

            var partido =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "A"),
                    new Seleccion(
                        "Brasil",
                        "A"),
                    fechaSistema.AddHours(5));

            var partidos =
                new List<Partido>
                {
                    partido
                };

            var resultado =
                service.ObtenerNotificacionesPartidosPendientes(
                    null,
                    partidos,
                    fechaSistema);

            Assert.AreEqual(
                0,
                resultado.Count);
        }

        [TestMethod]
        public void ObtenerNotificacionesPartidosPendientes_ListaVacia_DebeRetornarListaVacia()
        {
            var service = new NotificacionService();

            var fechaSistema =
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    50);

            var resultado =
                service.ObtenerNotificacionesPartidosPendientes(
                    usuario,
                    new List<Partido>(),
                    fechaSistema);

            Assert.AreEqual(
                0,
                resultado.Count);
        }
    }
}