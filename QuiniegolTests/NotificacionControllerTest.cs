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

        // =========================================================
        // PRUEBA 1
        // Cargar las notificaciones
        // =========================================================

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

            Assert.AreEqual(
                2,
                notificaciones.Count);
        }

        // =========================================================
        // PRUEBA 2
        // Buscar una notificación existente
        // =========================================================

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
                new DateTime(
                    2026,
                    8,
                    25,
                    18,
                    0,
                    0),
                notificacion.Fecha);
        }

        // =========================================================
        // PRUEBA 3
        // Buscar una notificación inexistente
        // =========================================================

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

        // =========================================================
        // PRUEBA 4
        // Cargar un archivo inexistente
        // =========================================================

        [TestMethod]
        public void Load_DebeRetornarListaVaciaSiArchivoNoExiste()
        {
            var dataHandler =
                new FileHandler<Notificacion>();

            var controller =
                new NotificacionController(dataHandler);

            var archivoInexistente =
                Path.Combine(
                    Path.GetTempPath(),
                    "ArchivoInexistente_" +
                    Guid.NewGuid().ToString() +
                    ".csv");

            var resultado =
                controller.Load(
                    archivoInexistente);

            Assert.IsNotNull(resultado);

            Assert.AreEqual(
                0,
                resultado.Count);
        }

        // =========================================================
        // PRUEBA 5
        // Crear una notificación
        // =========================================================

        [TestMethod]
        public void Create_DebeGuardarNotificacion()
        {
            var dataHandler =
                new FileHandler<Notificacion>();

            var notificacion =
                new Notificacion(
                    "Partido finalizado",
                    new DateTime(
                        2026,
                        8,
                        25,
                        20,
                        0,
                        0));

            var resultado =
                dataHandler.Create(
                    archivoPrueba,
                    notificacion);

            Assert.IsTrue(resultado);

            var controller =
                new NotificacionController(
                    dataHandler);

            var notificaciones =
                controller.Load(
                    archivoPrueba);

            Assert.IsNotNull(
                notificaciones);

            Assert.AreEqual(
                3,
                notificaciones.Count);

            var encontrada =
                controller.FindNotification(
                    "Partido finalizado");

            Assert.IsNotNull(encontrada);

            Assert.AreEqual(
                new DateTime(
                    2026,
                    8,
                    25,
                    20,
                    0,
                    0),
                encontrada.Fecha);
        }
    }
}