using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiniegol.Services;
using QuiniegolModels;
using System;

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
    }
}