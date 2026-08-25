using QuiniegolModels;
using System;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de manejar el reloj simulado
    /// y consultar el estado de los partidos.
    /// </summary>
    public class RelojService
    {
        /// <summary>
        /// Obtiene la fecha y hora actual del reloj simulado.
        /// </summary>
        public DateTime ObtenerFechaHoraActual()
        {
            return Reloj.Instance.CurrentDateTime;
        }

        /// <summary>
        /// Determina si un partido todavía acepta pronósticos.
        /// </summary>
        /// <param name="partido">Partido que se desea consultar.</param>
        /// <returns>
        /// True si el partido todavía acepta pronósticos;
        /// de lo contrario, false.
        /// </returns>
        public bool AceptaPronosticos(Partido partido)
        {
            if (partido == null)
            {
                return false;
            }

            return partido.AceptaPronosticos(
                Reloj.Instance.CurrentDateTime);
        }

        /// <summary>
        /// Determina si un partido está actualmente en curso.
        /// </summary>
        /// <param name="partido">Partido que se desea consultar.</param>
        /// <returns>
        /// True si el partido está en curso;
        /// de lo contrario, false.
        /// </returns>
        public bool EstaEnCurso(Partido partido)
        {
            if (partido == null)
            {
                return false;
            }

            return partido.EstaEnCurso(
                Reloj.Instance.CurrentDateTime);
        }

        /// <summary>
        /// Avanza el reloj simulado.
        /// </summary>
        /// <param name="cantidad">
        /// Cantidad de tiempo que se desea avanzar.
        /// </param>
        public void AvanzarTiempo(TimeSpan cantidad)
        {
            if (cantidad <= TimeSpan.Zero)
            {
                return;
            }

            Reloj.Instance.AdvanceTime(cantidad);
        }
    }
}