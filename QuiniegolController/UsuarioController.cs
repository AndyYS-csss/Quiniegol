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
        /// Inicializa una nueva instancia de la clase UsuarioController.
        /// </summary>
        /// <param name="dataHandler">Manejador de datos.</param>
        public UsuarioController(IDataHandler<Usuario> dataHandler)
        {
            DataHandler = dataHandler;
        }

        /// <summary>
        /// Obtiene los usuarios cargados.
        /// </summary>
        public List<Usuario> Usuarios { get; private set; }

        /// <summary>
        /// Carga los usuarios desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de usuarios.</returns>
        public List<Usuario> Load(string fileName)
        {
            var usuarios = this.DataHandler.Load(fileName);

            if (usuarios != null && usuarios.Count > 0)
            {
                this.Usuarios = usuarios;
                return usuarios;
            }

            return new List<Usuario>();
        }

        /// <summary>
        /// Busca un usuario por su país preferido.
        /// </summary>
        /// <param name="paisPreferido">País preferido.</param>
        /// <returns>Usuario encontrado o null.</returns>
        public Usuario FindUser(string paisPreferido)
        {
            if (this.Usuarios != null && this.Usuarios.Count > 0)
            {
                return this.Usuarios.Find(
                    usuario => usuario.PaisPreferido == paisPreferido);
            }

            return null;
        }

        /// <summary>
        /// Actualiza los puntos de un usuario.
        /// </summary>
        /// <param name="paisPreferido">País preferido del usuario.</param>
        /// <param name="puntos">Nueva cantidad de puntos.</param>
        /// <returns>True si se actualizó; de lo contrario, false.</returns>
        public bool UpdatePoints(string paisPreferido, int puntos)
        {
            var usuario = this.FindUser(paisPreferido);

            if (usuario == null)
            {
                return false;
            }

            usuario.Puntos = puntos;
            return true;
        }
    }
}