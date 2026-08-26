using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolWeb.Services
{
    /// <summary>
    /// Gestiona la sesión del usuario actual dentro de la aplicación web.
    /// </summary>
    public class SesionService
    {
        /// <summary>
        /// Obtiene el usuario que actualmente tiene la sesión iniciada.
        /// </summary>
        public Usuario UsuarioActual { get; private set; }

        /// <summary>
        /// Lista de usuarios disponibles para iniciar sesión.
        /// </summary>
        public List<Usuario> Usuarios { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia del servicio de sesión.
        /// </summary>
        public SesionService()
        {
            Usuarios = new List<Usuario>();
        }

        /// <summary>
        /// Indica si existe una sesión iniciada.
        /// </summary>
        public bool SesionIniciada
        {
            get
            {
                return UsuarioActual != null;
            }
        }

        /// <summary>
        /// Agrega un usuario al sistema de sesión.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que se agregará.
        /// </param>
        public void AgregarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            if (Usuarios.Exists(
                existente => existente.Nombre == usuario.Nombre))
            {
                return;
            }

            Usuarios.Add(usuario);
        }

        /// <summary>
        /// Busca un usuario por su nombre.
        /// </summary>
        /// <param name="nombre">
        /// Nombre del usuario que se desea buscar.
        /// </param>
        /// <returns>
        /// Usuario encontrado o null.
        /// </returns>
        public Usuario BuscarUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return null;
            }

            return Usuarios.Find(
                usuario => usuario.Nombre == nombre);
        }

        /// <summary>
        /// Inicia la sesión de un usuario.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que iniciará sesión.
        /// </param>
        public void IniciarSesion(Usuario usuario)
        {
            if (usuario == null)
            {
                return;
            }

            if (!usuario.Activo)
            {
                return;
            }

            UsuarioActual = usuario;
        }

        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        public void CerrarSesion()
        {
            UsuarioActual = null;
        }

        /// <summary>
        /// Determina si el usuario actual es administrador.
        /// </summary>
        /// <returns>
        /// True si el usuario tiene rol de administrador.
        /// </returns>
        public bool EsAdministrador()
        {
            if (UsuarioActual == null)
            {
                return false;
            }

            return UsuarioActual.Rol == "Administrador";
        }
    }
}