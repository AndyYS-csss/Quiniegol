using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuiniegolController;
using QuiniegolModels;
using System;
using System.IO;

namespace QuiniegolTests
{
    [TestClass]
    public class QuinielaControllerTest
    {
        private string archivoPrueba = string.Empty;

        [TestInitialize]
        public void Inicializar()
        {
            archivoPrueba = Path.Combine(
                Path.GetTempPath(),
                "QuinielasTest_" +
                Guid.NewGuid().ToString() +
                ".csv");

            File.WriteAllText(
                archivoPrueba,
                "Nombre,EsPrivada" +
                Environment.NewLine +
                "Quiniela Amigos,true" +
                Environment.NewLine +
                "Quiniela Publica,false");
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
        public void Load_DebeCargarLasQuinielas()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            var quinielas =
                controller.Load(archivoPrueba);

            Assert.IsNotNull(quinielas);
            Assert.AreEqual(2, quinielas.Count);
        }

        [TestMethod]
        public void FindQuiniela_DebeEncontrarQuinielaExistente()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var quiniela =
                controller.FindQuiniela(
                    "Quiniela Amigos");

            Assert.IsNotNull(quiniela);
            Assert.AreEqual(
                "Quiniela Amigos",
                quiniela.Nombre);

            Assert.IsTrue(quiniela.EsPrivada);
        }

        [TestMethod]
        public void FindQuiniela_DebeRetornarNullSiNoExiste()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var quiniela =
                controller.FindQuiniela(
                    "Quiniela Inexistente");

            Assert.IsNull(quiniela);
        }

        [TestMethod]
        public void CreateQuiniela_DebeCrearNuevaQuiniela()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.CreateQuiniela(
                    archivoPrueba,
                    "Quiniela Nueva",
                    true);

            Assert.IsTrue(resultado);

            var quiniela =
                controller.FindQuiniela(
                    "Quiniela Nueva");

            Assert.IsNotNull(quiniela);
            Assert.AreEqual(
                "Quiniela Nueva",
                quiniela.Nombre);

            Assert.IsTrue(quiniela.EsPrivada);
        }

        [TestMethod]
        public void CreateQuiniela_NoDebeAceptarNombreVacio()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.CreateQuiniela(
                    archivoPrueba,
                    "",
                    true);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void CreateQuiniela_NoDebeRegistrarDuplicada()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.CreateQuiniela(
                    archivoPrueba,
                    "Quiniela Amigos",
                    true);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void AddIntegrante_DebeAgregarUsuario()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    100);

            var resultado =
                controller.AddIntegrante(
                    "Quiniela Amigos",
                    usuario);

            Assert.IsTrue(resultado);
            Assert.AreEqual(
                1,
                controller.FindQuiniela(
                    "Quiniela Amigos").Integrantes.Count);
        }

        [TestMethod]
        public void AddIntegrante_NoDebeAgregarUsuarioDuplicado()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    100);

            controller.AddIntegrante(
                "Quiniela Amigos",
                usuario);

            var resultado =
                controller.AddIntegrante(
                    "Quiniela Amigos",
                    usuario);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void AddIntegrante_DebeRetornarFalseSiQuinielaNoExiste()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    100);

            var resultado =
                controller.AddIntegrante(
                    "Quiniela Inexistente",
                    usuario);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void EsIntegrante_DebeRetornarTrueSiUsuarioPertenece()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    100);

            controller.AddIntegrante(
                "Quiniela Amigos",
                usuario);

            var resultado =
                controller.EsIntegrante(
                    "Quiniela Amigos",
                    "Carlos");

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void EsIntegrante_DebeRetornarFalseSiUsuarioNoPertenece()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.EsIntegrante(
                    "Quiniela Amigos",
                    "UsuarioInexistente");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ObtenerPosiciones_DebeOrdenarPorPuntos()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario1 =
                new Usuario(
                    "Carlos",
                    "Costa Rica",
                    100);

            var usuario2 =
                new Usuario(
                    "Ana",
                    "Brasil",
                    300);

            var usuario3 =
                new Usuario(
                    "Luis",
                    "Argentina",
                    200);

            controller.AddIntegrante(
                "Quiniela Amigos",
                usuario1);

            controller.AddIntegrante(
                "Quiniela Amigos",
                usuario2);

            controller.AddIntegrante(
                "Quiniela Amigos",
                usuario3);

            var posiciones =
                controller.ObtenerPosiciones(
                    "Quiniela Amigos");

            Assert.AreEqual(3, posiciones.Count);

            Assert.AreEqual(
                "Ana",
                posiciones[0].Nombre);

            Assert.AreEqual(
                300,
                posiciones[0].Puntos);

            Assert.AreEqual(
                "Luis",
                posiciones[1].Nombre);

            Assert.AreEqual(
                200,
                posiciones[1].Puntos);

            Assert.AreEqual(
                "Carlos",
                posiciones[2].Nombre);

            Assert.AreEqual(
                100,
                posiciones[2].Puntos);
        }

        [TestMethod]
        public void ObtenerPosiciones_DebeRetornarListaVaciaSiNoExiste()
        {
            var dataHandler = new FileHandler<Quiniela>();
            var controller =
                new QuinielaController(dataHandler);

            controller.Load(archivoPrueba);

            var posiciones =
                controller.ObtenerPosiciones(
                    "Quiniela Inexistente");

            Assert.IsNotNull(posiciones);
            Assert.AreEqual(0, posiciones.Count);
        }
    }
}