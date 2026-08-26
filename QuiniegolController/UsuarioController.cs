using QuiniegolController.Abstractions;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolController
{
    /// <summary>
    /// Clase encargada de las operaciones de los usuarios.
    /// </summary>
    public class UsuarioController
    {
        private IDataHandler<Usuario> DataHandler { get; set; }

        /// <summary>
        /// Obtiene los usuarios cargados.
        /// </summary>
        public List<Usuario> Usuarios { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase UsuarioController.
        /// </summary>
        /// <param name="dataHandler">Manejador de datos.</param>
        public UsuarioController(IDataHandler<Usuario> dataHandler)
        {
            DataHandler = dataHandler;
            Usuarios = new List<Usuario>();
        }

        /// <summary>
        /// Carga los usuarios desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de usuarios.</returns>
        public List<Usuario> Load(string fileName)
        {
            var usuarios = this.DataHandler.Load(fileName);

            if (usuarios != null)
            {
                this.Usuarios = usuarios;
                return usuarios;
            }

            return new List<Usuario>();
        }

        /// <summary>
        /// Busca un usuario por su nombre.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <returns>Usuario encontrado o null.</returns>
        public Usuario FindUser(string nombre)
        {
            if (this.Usuarios != null &&
                this.Usuarios.Count > 0)
            {
                return this.Usuarios.Find(
                    usuario => usuario.Nombre == nombre);
            }

            return null;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="fileName">Archivo donde se guardará el usuario.</param>
        /// <param name="nombre">Nombre completo del usuario.</param>
        /// <param name="paisPreferido">País preferido.</param>
        /// <returns>True si el usuario fue registrado correctamente.</returns>
        public bool RegisterUser(
            string fileName,
            string nombre,
            string paisPreferido)
        {
            return RegisterUser(
                fileName,
                nombre,
                paisPreferido,
                string.Empty);
        }

        /// <summary>
        /// Registra un nuevo usuario con contraseña.
        /// </summary>
        /// <param name="fileName">Archivo donde se guardará el usuario.</param>
        /// <param name="nombre">Nombre completo del usuario.</param>
        /// <param name="paisPreferido">País preferido.</param>
        /// <param name="contrasena">Contraseña del usuario.</param>
        /// <returns>True si el usuario fue registrado correctamente.</returns>
        public bool RegisterUser(
            string fileName,
            string nombre,
            string paisPreferido,
            string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(paisPreferido))
            {
                return false;
            }

            if (this.FindUser(nombre) != null)
            {
                return false;
            }

            var usuario = new Usuario(
                nombre,
                paisPreferido,
                0);

            usuario.Contrasena = contrasena ?? string.Empty;
            usuario.Rol = "Usuario";
            usuario.Activo = true;

            var created = this.DataHandler.Create(
                fileName,
                usuario);

            if (!created)
            {
                return false;
            }

            this.Usuarios.Add(usuario);

            return true;
        }

        /// <summary>
        /// Actualiza los puntos de un usuario.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="puntos">Nueva cantidad de puntos.</param>
        /// <returns>True si se actualizó; de lo contrario, false.</returns>
        public bool UpdatePoints(
            string nombre,
            int puntos)
        {
            var usuario = this.FindUser(nombre);

            if (usuario == null)
            {
                return false;
            }

            usuario.Puntos = puntos;

            return true;
        }

        /// <summary>
        /// Autentica un usuario utilizando su nombre y contraseña.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="contrasena">Contraseña proporcionada.</param>
        /// <returns>
        /// El usuario autenticado si los datos son correctos;
        /// de lo contrario, null.
        /// </returns>
        public Usuario AuthenticateUser(
            string nombre,
            string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                return null;
            }

            var usuario = this.FindUser(nombre);

            if (usuario == null)
            {
                return null;
            }

            if (!usuario.Activo)
            {
                return null;
            }

            if (usuario.Contrasena != contrasena)
            {
                return null;
            }

            return usuario;
        }

        /// <summary>
        /// Cambia o restablece la contraseña de un usuario.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña.</param>
        /// <returns>
        /// True si la contraseña fue cambiada correctamente.
        /// </returns>
        public bool ResetPassword(
            string nombre,
            string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(nuevaContrasena))
            {
                return false;
            }

            var usuario = this.FindUser(nombre);

            if (usuario == null)
            {
                return false;
            }

            usuario.Contrasena = nuevaContrasena;

            return true;
        }

        /// <summary>
        /// Desactiva un usuario.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <returns>
        /// True si el usuario fue desactivado correctamente.
        /// </returns>
        public bool DeactivateUser(string nombre)
        {
            var usuario = this.FindUser(nombre);

            if (usuario == null)
            {
                return false;
            }

            usuario.Activo = false;

            return true;
        }

        /// <summary>
        /// Activa nuevamente un usuario.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <returns>
        /// True si el usuario fue activado correctamente.
        /// </returns>
        public bool ActivateUser(string nombre)
        {
            var usuario = this.FindUser(nombre);

            if (usuario == null)
            {
                return false;
            }

            usuario.Activo = true;

            return true;
        }

        /// <summary>
        /// Determina si un usuario tiene el rol de administrador.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <returns>
        /// True si el usuario es administrador.
        /// </returns>
        public bool IsAdministrator(string nombre)
        {
            var usuario = this.FindUser(nombre);

            if (usuario == null)
            {
                return false;
            }

            return usuario.Rol == "Administrador";
        }
    }
}