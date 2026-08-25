using QuiniegolController.Abstractions;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolController
{
    /// <summary>
    /// Clase encargada de las operaciones de las notificaciones.
    /// </summary>
    public class NotificacionController
    {
        private IDataHandler<Notificacion> DataHandler { get; set; }

        /// <summary>
        /// Obtiene las notificaciones cargadas.
        /// </summary>
        public List<Notificacion> Notificaciones { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase NotificacionController.
        /// </summary>
        /// <param name="dataHandler">Manejador de datos.</param>
        public NotificacionController(
            IDataHandler<Notificacion> dataHandler)
        {
            DataHandler = dataHandler;
            Notificaciones = new List<Notificacion>();
        }

        /// <summary>
        /// Carga las notificaciones desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de notificaciones.</returns>
        public List<Notificacion> Load(string fileName)
        {
            var notificaciones = this.DataHandler.Load(fileName);

            if (notificaciones != null)
            {
                this.Notificaciones = notificaciones;
                return notificaciones;
            }

            return new List<Notificacion>();
        }

        /// <summary>
        /// Busca una notificación por su mensaje.
        /// </summary>
        /// <param name="mensaje">Mensaje de la notificación.</param>
        /// <returns>Notificación encontrada o null.</returns>
        public Notificacion FindNotification(string mensaje)
        {
            if (this.Notificaciones == null ||
                this.Notificaciones.Count == 0)
            {
                return null;
            }

            return this.Notificaciones.Find(
                notificacion => notificacion.Mensaje == mensaje);
        }
    }
}