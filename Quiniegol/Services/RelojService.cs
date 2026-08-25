using QuiniegolModels;
using System;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de controlar la fecha y hora
    /// simulada del sistema.
    /// </summary>
    public class RelojService
    {
        /// <summary>
        /// Obtiene la fecha y hora actual del reloj simulado.
        /// </summary>
        /// <returns>
        /// Fecha y hora actual del sistema simulado.
        /// </returns>
        public DateTime ObtenerFechaHoraActual()
        {
            return Reloj.Instance.CurrentDateTime;
        }

        /// <summary>
        /// Avanza el reloj simulado una cantidad determinada
        /// de tiempo.
        /// </summary>
        /// <param name="cantidad">
        /// Cantidad de tiempo que se desea avanzar.
        /// </param>
        public void AvanzarTiempo(TimeSpan cantidad)
        {
            // No se permite retroceder el reloj
            // ni avanzar una cantidad de tiempo igual a cero.
            if (cantidad <= TimeSpan.Zero)
            {
                return;
            }

            Reloj.Instance.AdvanceTime(cantidad);
        }

        /// <summary>
        /// Determina si un partido todavía acepta pronósticos.
        /// </summary>
        /// <param name="partido">
        /// Partido que se desea consultar.
        /// </param>
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
                ObtenerFechaHoraActual());
        }

        /// <summary>
        /// Determina si un partido se encuentra en curso.
        /// </summary>
        /// <param name="partido">
        /// Partido que se desea consultar.
        /// </param>
        /// <returns>
        /// True si el partido ya inició;
        /// de lo contrario, false.
        /// </returns>
        public bool EstaEnCurso(Partido partido)
        {
            if (partido == null)
            {
                return false;
            }

            return !partido.AceptaPronosticos(
                ObtenerFechaHoraActual());
        }
    }
}