using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuiniegolController;
using QuiniegolModels;
using System;
using System.IO;

namespace QuiniegolTests
{
    [TestClass]
    public class NotificacionControllerTest
    {
        private string archivoPrueba = string.Empty;

        [TestInitialize]
        public void Inicializar()
        {
            archivoPrueba = Path.Combine(
                Path.GetTempPath(),
                "NotificacionesTest_" +
                Guid.NewGuid().ToString() +
                ".csv");

            File.WriteAllText(
                archivoPrueba,
                "Mensaje,Fecha" +
                Environment.NewLine +
                "Bienvenido a la quiniela,2026-08-25 18:00:00" +
                Environment.NewLine +
                "Nuevo resultado disponible,2026-08-25 19:00:00");
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
        public void Load_DebeCargarLasNotificaciones()
        {
            var dataHandler =
                new FileHandler<Notificacion>();

            var controller =
                new NotificacionController(dataHandler);

            var notificaciones =
                controller.Load(archivoPrueba);

            Assert.IsNotNull(notificaciones);
            Assert.AreEqual(2, notificaciones.Count);
        }

        [TestMethod]
        public void FindNotification_DebeEncontrarNotificacionExistente()
        {
            var dataHandler =
                new FileHandler<Notificacion>();

            var controller =
                new NotificacionController(dataHandler);

            controller.Load(archivoPrueba);

            var notificacion =
                controller.FindNotification(
                    "Bienvenido a la quiniela");

            Assert.IsNotNull(notificacion);

            Assert.AreEqual(
                "Bienvenido a la quiniela",
                notificacion.Mensaje);

            Assert.AreEqual(
                new DateTime(2026, 8, 25, 18, 0, 0),
                notificacion.Fecha);
        }

        [TestMethod]
        public void FindNotification_DebeRetornarNullSiNoExiste()
        {
            var dataHandler =
                new FileHandler<Notificacion>();

            var controller =
                new NotificacionController(dataHandler);

            controller.Load(archivoPrueba);

            var notificacion =
                controller.FindNotification(
                    "Notificacion Inexistente");

            Assert.IsNull(notificacion);
        }
    }
}