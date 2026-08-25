using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuiniegolController;
using QuiniegolModels;
using System;
using System.IO;

namespace QuiniegolTests
{
    [TestClass]
    public class UsuarioControllerTest
    {
        private string archivoPrueba = string.Empty;

        [TestInitialize]
        public void Inicializar()
        {
            archivoPrueba = Path.Combine(
                Path.GetTempPath(),
                "UsuariosTest_" +
                Guid.NewGuid().ToString() +
                ".csv");

            File.WriteAllText(
                archivoPrueba,
                "Nombre,PaisPreferido,Puntos" +
                Environment.NewLine +
                "Carlos,Costa Rica,100" +
                Environment.NewLine +
                "Ana,Mexico,200");
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
        public void Load_DebeCargarLosUsuarios()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            var usuarios = controller.Load(archivoPrueba);

            Assert.IsNotNull(usuarios);
            Assert.AreEqual(2, usuarios.Count);
        }

        [TestMethod]
        public void FindUser_DebeEncontrarUsuarioExistente()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario = controller.FindUser("Carlos");

            Assert.IsNotNull(usuario);
            Assert.AreEqual("Carlos", usuario.Nombre);
            Assert.AreEqual("Costa Rica", usuario.PaisPreferido);
            Assert.AreEqual(100, usuario.Puntos);
        }

        [TestMethod]
        public void FindUser_DebeRetornarNullSiNoExiste()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario =
                controller.FindUser("UsuarioInexistente");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void RegisterUser_DebeRegistrarNuevoUsuario()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.RegisterUser(
                archivoPrueba,
                "Luis",
                "Panama");

            Assert.IsTrue(resultado);

            var usuario = controller.FindUser("Luis");

            Assert.IsNotNull(usuario);
            Assert.AreEqual("Luis", usuario.Nombre);
            Assert.AreEqual("Panama", usuario.PaisPreferido);
            Assert.AreEqual(0, usuario.Puntos);
        }

        [TestMethod]
        public void RegisterUser_NoDebeRegistrarUsuarioDuplicado()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.RegisterUser(
                archivoPrueba,
                "Carlos",
                "Costa Rica");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void RegisterUser_NoDebeAceptarDatosVacios()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.RegisterUser(
                archivoPrueba,
                "",
                "Costa Rica");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void UpdatePoints_DebeActualizarLosPuntos()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.UpdatePoints(
                "Carlos",
                500);

            Assert.IsTrue(resultado);

            var usuario =
                controller.FindUser("Carlos");

            Assert.IsNotNull(usuario);
            Assert.AreEqual(500, usuario.Puntos);
        }

        [TestMethod]
        public void UpdatePoints_DebeRetornarFalseSiUsuarioNoExiste()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.UpdatePoints(
                "UsuarioInexistente",
                500);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void RegisterUser_DebePermitirUsuarioAdicional()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.RegisterUser(
                archivoPrueba,
                "Usuario Nuevo",
                "Costa Rica");

            Assert.IsTrue(resultado);

            var usuario =
                controller.FindUser("Usuario Nuevo");

            Assert.IsNotNull(usuario);
            Assert.AreEqual(
                "Usuario Nuevo",
                usuario.Nombre);

            Assert.AreEqual(
                "Costa Rica",
                usuario.PaisPreferido);

            Assert.AreEqual(
                0,
                usuario.Puntos);
        }
    }
}