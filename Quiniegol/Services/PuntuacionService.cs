using QuiniegolModels;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de calcular y actualizar
    /// los puntos obtenidos por un pronóstico.
    /// </summary>
    public class PuntuacionService
    {
        /// <summary>
        /// Calcula los puntos obtenidos por un pronóstico
        /// comparándolo con el resultado final del partido.
        /// </summary>
        /// <param name="pronostico">
        /// Pronóstico realizado por el usuario.
        /// </param>
        /// <param name="partido">
        /// Partido con su resultado final.
        /// </param>
        /// <returns>
        /// 5 puntos si el marcador es exacto,
        /// 2 puntos si acierta el ganador o empate,
        /// 0 puntos si no acierta.
        /// </returns>
        public int CalcularPuntos(
            Pronostico pronostico,
            Partido partido)
        {
            if (pronostico == null ||
                partido == null)
            {
                return 0;
            }

            // Marcador exacto.
            if (pronostico.GolesLocal ==
                    partido.GolesLocal &&
                pronostico.GolesVisitante ==
                    partido.GolesVisitante)
            {
                return 5;
            }

            // Determina el resultado pronosticado.
            int resultadoPronosticado =
                ObtenerResultado(
                    pronostico.GolesLocal,
                    pronostico.GolesVisitante);

            // Determina el resultado real.
            int resultadoReal =
                ObtenerResultado(
                    partido.GolesLocal,
                    partido.GolesVisitante);

            // Acertó ganador o empate.
            if (resultadoPronosticado ==
                resultadoReal)
            {
                return 2;
            }

            // No acertó.
            return 0;
        }

        /// <summary>
        /// Calcula y actualiza los puntos del pronóstico.
        /// </summary>
        /// <param name="pronostico">
        /// Pronóstico cuyos puntos se desean actualizar.
        /// </param>
        /// <param name="partido">
        /// Partido con el resultado final.
        /// </param>
        /// <returns>
        /// True si el pronóstico fue actualizado;
        /// de lo contrario, false.
        /// </returns>
        public bool ActualizarPuntosPronostico(
            Pronostico pronostico,
            Partido partido)
        {
            if (pronostico == null ||
                partido == null)
            {
                return false;
            }

            int puntos =
                CalcularPuntos(
                    pronostico,
                    partido);

            pronostico.Puntos = puntos;

            return true;
        }

        /// <summary>
        /// Determina el resultado de un marcador.
        /// </summary>
        /// <param name="golesLocal">
        /// Goles del equipo local.
        /// </param>
        /// <param name="golesVisitante">
        /// Goles del equipo visitante.
        /// </param>
        /// <returns>
        /// 1 si gana el local,
        /// -1 si gana el visitante,
        /// 0 si es empate.
        /// </returns>
        private int ObtenerResultado(
            int golesLocal,
            int golesVisitante)
        {
            if (golesLocal > golesVisitante)
            {
                return 1;
            }

            if (golesLocal < golesVisitante)
            {
                return -1;
            }

            return 0;
        }
    }
}