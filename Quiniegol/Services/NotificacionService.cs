using QuiniegolModels;
using System;
using System.Collections.Generic;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las notificaciones
    /// del timeline de cada quiniela y las notificaciones
    /// de partidos pendientes de pronosticar.
    /// </summary>
    public class NotificacionService
    {
        /// <summary>
        /// Agrega una notificación al timeline de una quiniela.
        /// </summary>
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

        /// <summary>
        /// Obtiene las notificaciones de los partidos que
        /// todavía no han sido pronosticados por el usuario
        /// y que se encuentran dentro de las próximas 24 horas.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que inició sesión.
        /// </param>
        /// <param name="partidos">
        /// Partidos disponibles del sistema.
        /// </param>
        /// <param name="fechaSistema">
        /// Fecha y hora simulada del sistema.
        /// </param>
        /// <returns>
        /// Lista de notificaciones para los partidos pendientes.
        /// </returns>
        public List<Notificacion>
            ObtenerNotificacionesPartidosPendientes(
                Usuario usuario,
                List<Partido> partidos,
                DateTime fechaSistema)
        {
            var notificaciones =
                new List<Notificacion>();

            if (usuario == null ||
                partidos == null ||
                partidos.Count == 0)
            {
                return notificaciones;
            }

            DateTime limite24Horas =
                fechaSistema.AddHours(24);

            foreach (Partido partido in partidos)
            {
                if (partido == null ||
                    partido.Local == null ||
                    partido.Visitante == null)
                {
                    continue;
                }

                // El partido debe comenzar después
                // de la fecha actual.
                if (partido.Fecha <= fechaSistema)
                {
                    continue;
                }

                // El partido debe comenzar dentro
                // de las próximas 24 horas.
                if (partido.Fecha > limite24Horas)
                {
                    continue;
                }

                // No se consideran partidos finalizados.
                if (partido.Finalizado)
                {
                    continue;
                }

                // Si el usuario ya realizó un pronóstico
                // para este partido, no se genera notificación.
                if (TienePronostico(
                        usuario,
                        partido))
                {
                    continue;
                }

                string mensaje =
                    $"Falta realizar el pronóstico del partido " +
                    $"{partido.Local.Nombre} vs " +
                    $"{partido.Visitante.Nombre}, " +
                    $"programado para " +
                    $"{partido.Fecha:dd/MM/yyyy HH:mm}.";

                notificaciones.Add(
                    new Notificacion(
                        mensaje,
                        fechaSistema));
            }

            return notificaciones;
        }

        /// <summary>
        /// Determina si el usuario ya realizó un pronóstico
        /// para el partido indicado.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que será evaluado.
        /// </param>
        /// <param name="partido">
        /// Partido que será evaluado.
        /// </param>
        /// <returns>
        /// True si ya existe un pronóstico para el partido.
        /// </returns>
        private bool TienePronostico(
            Usuario usuario,
            Partido partido)
        {
            if (usuario == null ||
                usuario.Pronosticos == null ||
                partido == null ||
                partido.Local == null ||
                partido.Visitante == null)
            {
                return false;
            }

            foreach (Pronostico pronostico
                in usuario.Pronosticos)
            {
                if (pronostico == null)
                {
                    continue;
                }

                if (pronostico.NombreUsuario !=
                    usuario.Nombre)
                {
                    continue;
                }

                if (pronostico.Local ==
                    partido.Local.Nombre &&
                    pronostico.Visitante ==
                    partido.Visitante.Nombre)
                {
                    return true;
                }
            }

            return false;
        }
    }
}