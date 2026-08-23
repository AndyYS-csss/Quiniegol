using System;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa el pronóstico de un usuario para un partido.
    /// </summary>
    public class Pronostico
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Pronostico.
        /// </summary>
        /// <param name="golesLocal">Goles pronosticados para el equipo local.</param>
        /// <param name="golesVisitante">Goles pronosticados para el equipo visitante.</param>
        public Pronostico(int golesLocal, int golesVisitante)
        {
            this.GolesLocal = golesLocal;
            this.GolesVisitante = golesVisitante;
            this.Puntos = 0;
        }

        /// <summary>
        /// Obtiene o establece los goles pronosticados para el equipo local.
        /// </summary>
        public int GolesLocal { get; set; }

        /// <summary>
        /// Obtiene o establece los goles pronosticados para el equipo visitante.
        /// </summary>
        public int GolesVisitante { get; set; }

        /// <summary>
        /// Obtiene o establece los puntos obtenidos por el pronóstico.
        /// </summary>
        public int Puntos { get; set; }
    }
}