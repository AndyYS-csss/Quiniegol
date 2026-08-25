using QuiniegolModels;
using System;
using System.Collections.Generic;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las notificaciones
    /// del timeline de cada quiniela.
    /// </summary>
    public class NotificacionService
    {
        /// <summary>
        /// Agrega una notificación al timeline de una quiniela.
        /// </summary>
        /// <param name="quiniela">
        /// Quiniela donde se agregará la notificación.
        /// </param>
        /// <param name="mensaje">
        /// Mensaje que se mostrará en el timeline.
        /// </param>
        /// <param name="fecha">
        /// Fecha y hora de la notificación.
        /// </param>
        /// <returns>
        /// True si la notificación fue agregada correctamente.
        /// </returns>
        public bool AgregarNotificacion(
            Quiniela quiniela,
            string mensaje,
            DateTime fecha)
        {
            if (quiniela == null ||
                string.IsNullOrWhiteSpace(mensaje))
            {
                return false;
            }

            if (quiniela.Notificaciones == null)
            {
                quiniela.Notificaciones =
                    new List<Notificacion>();
            }

            quiniela.Notificaciones.Add(
                new Notificacion(
                    mensaje,
                    fecha));

            return true;
        }

        /// <summary>
        /// Genera una notificación indicando que un jugador
        /// acertó el marcador de un partido.
        /// </summary>
        public bool NotificarMarcadorAcertado(
            Quiniela quiniela,
            Usuario usuario,
            Partido partido,
            DateTime fecha)
        {
            if (usuario == null ||
                partido == null)
            {
                return false;
            }

            string mensaje =
                $"{usuario.Nombre} acertó el marcador " +
                $"del partido {partido.Local.Nombre} " +
                $"vs {partido.Visitante.Nombre}.";

            return AgregarNotificacion(
                quiniela,
                mensaje,
                fecha);
        }

        /// <summary>
        /// Genera una notificación indicando que un jugador
        /// está en una racha de aciertos.
        /// </summary>
        public bool NotificarRacha(
            Quiniela quiniela,
            Usuario usuario,
            int cantidad,
            DateTime fecha)
        {
            if (usuario == null ||
                cantidad <= 0)
            {
                return false;
            }

            string mensaje =
                $"{usuario.Nombre} está en racha de " +
                $"{cantidad} aciertos.";

            return AgregarNotificacion(
                quiniela,
                mensaje,
                fecha);
        }

        /// <summary>
        /// Genera una notificación indicando que existe
        /// un nuevo líder en la quiniela.
        /// </summary>
        public bool NotificarNuevoLider(
            Quiniela quiniela,
            Usuario usuario,
            DateTime fecha)
        {
            if (usuario == null)
            {
                return false;
            }

            string mensaje =
                $"Nuevo líder: {usuario.Nombre}.";

            return AgregarNotificacion(
                quiniela,
                mensaje,
                fecha);
        }

        /// <summary>
        /// Genera una notificación relacionada con el jugador
        /// que ocupa la última posición de la quiniela.
        /// </summary>
        public bool NotificarVerguenza(
            Quiniela quiniela,
            Usuario usuario,
            DateTime fecha)
        {
            if (usuario == null)
            {
                return false;
            }

            string mensaje =
                $"{usuario.Nombre} ocupa actualmente " +
                $"la última posición de la quiniela.";

            return AgregarNotificacion(
                quiniela,
                mensaje,
                fecha);
        }

        /// <summary>
        /// Obtiene el timeline de notificaciones de una quiniela.
        /// </summary>
        /// <param name="quiniela">
        /// Quiniela de la cual se desean obtener las notificaciones.
        /// </param>
        /// <returns>
        /// Lista de notificaciones de la quiniela.
        /// </returns>
        public List<Notificacion> ObtenerTimeline(
            Quiniela quiniela)
        {
            if (quiniela == null ||
                quiniela.Notificaciones == null)
            {
                return new List<Notificacion>();
            }

            return quiniela.Notificaciones;
        }
    }
}