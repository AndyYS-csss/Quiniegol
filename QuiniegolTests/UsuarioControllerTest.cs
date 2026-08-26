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
                "Nombre,PaisPreferido,Puntos,Rol,Contrasena,Activo" +
                Environment.NewLine +
                "Carlos,Costa Rica,100,Usuario,1234,true" +
                Environment.NewLine +
                "Ana,Mexico,200,Administrador,admin123,true");
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
            Assert.AreEqual("Usuario", usuario.Rol);
            Assert.IsTrue(usuario.Activo);
        }

        [TestMethod]
        public void RegisterUser_DebeRegistrarUsuarioConContrasena()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.RegisterUser(
                archivoPrueba,
                "Luis",
                "Panama",
                "12345");

            Assert.IsTrue(resultado);

            var usuario = controller.FindUser("Luis");

            Assert.IsNotNull(usuario);
            Assert.AreEqual("12345", usuario.Contrasena);
            Assert.AreEqual("Usuario", usuario.Rol);
            Assert.IsTrue(usuario.Activo);
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

        [TestMethod]
        public void AuthenticateUser_DebeAutenticarUsuarioCorrectamente()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario = controller.AuthenticateUser(
                "Carlos",
                "1234");

            Assert.IsNotNull(usuario);
            Assert.AreEqual("Carlos", usuario.Nombre);
        }

        [TestMethod]
        public void AuthenticateUser_DebeRechazarContrasenaIncorrecta()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario = controller.AuthenticateUser(
                "Carlos",
                "incorrecta");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void AuthenticateUser_DebeRechazarUsuarioInexistente()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var usuario = controller.AuthenticateUser(
                "NoExiste",
                "1234");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void AuthenticateUser_DebeRechazarUsuarioDesactivado()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            controller.DeactivateUser("Carlos");

            var usuario = controller.AuthenticateUser(
                "Carlos",
                "1234");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void ResetPassword_DebeCambiarLaContrasena()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.ResetPassword(
                "Carlos",
                "nueva123");

            Assert.IsTrue(resultado);

            var usuario = controller.AuthenticateUser(
                "Carlos",
                "nueva123");

            Assert.IsNotNull(usuario);
        }

        [TestMethod]
        public void ResetPassword_DebeRetornarFalseSiUsuarioNoExiste()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.ResetPassword(
                "NoExiste",
                "nueva123");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ResetPassword_NoDebeAceptarContrasenaVacia()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.ResetPassword(
                "Carlos",
                "");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void DeactivateUser_DebeDesactivarUsuario()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.DeactivateUser("Carlos");

            Assert.IsTrue(resultado);

            var usuario = controller.FindUser("Carlos");

            Assert.IsNotNull(usuario);
            Assert.IsFalse(usuario.Activo);
        }

        [TestMethod]
        public void DeactivateUser_DebeRetornarFalseSiUsuarioNoExiste()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.DeactivateUser(
                "NoExiste");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ActivateUser_DebeActivarUsuario()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            controller.DeactivateUser("Carlos");

            var resultado = controller.ActivateUser("Carlos");

            Assert.IsTrue(resultado);

            var usuario = controller.FindUser("Carlos");

            Assert.IsNotNull(usuario);
            Assert.IsTrue(usuario.Activo);
        }

        [TestMethod]
        public void ActivateUser_DebeRetornarFalseSiUsuarioNoExiste()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado = controller.ActivateUser(
                "NoExiste");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsAdministrator_DebeRetornarTrueParaAdministrador()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.IsAdministrator("Ana");

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void IsAdministrator_DebeRetornarFalseParaUsuarioNormal()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.IsAdministrator("Carlos");

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void IsAdministrator_DebeRetornarFalseSiUsuarioNoExiste()
        {
            var dataHandler = new FileHandler<Usuario>();
            var controller = new UsuarioController(dataHandler);

            controller.Load(archivoPrueba);

            var resultado =
                controller.IsAdministrator("NoExiste");

            Assert.IsFalse(resultado);
        }
    }
}