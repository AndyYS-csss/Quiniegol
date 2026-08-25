using System.Collections.Generic;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa una quiniela dentro del sistema.
    /// </summary>
    public class Quiniela
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Quiniela.
        /// </summary>
        /// <param name="nombre">
        /// Nombre de la quiniela.
        /// </param>
        /// <param name="esPrivada">
        /// Indica si la quiniela es privada.
        /// </param>
        public Quiniela(
            string nombre,
            bool esPrivada)
        {
            this.Nombre = nombre;
            this.EsPrivada = esPrivada;
            this.Integrantes = new List<Usuario>();
            this.Notificaciones = new List<Notificacion>();
        }

        /// <summary>
        /// Obtiene o establece el nombre de la quiniela.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece si la quiniela es privada.
        /// </summary>
        public bool EsPrivada { get; set; }

        /// <summary>
        /// Obtiene los usuarios que pertenecen
        /// a la quiniela.
        /// </summary>
        public List<Usuario> Integrantes { get; set; }

        /// <summary>
        /// Obtiene las notificaciones de la quiniela.
        /// </summary>
        public List<Notificacion> Notificaciones { get; set; }
    }
}