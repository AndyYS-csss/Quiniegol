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
        /// <param name="nombreUsuario">Nombre del usuario que realiza el pronóstico.</param>
        /// <param name="local">Nombre de la selección local.</param>
        /// <param name="visitante">Nombre de la selección visitante.</param>
        /// <param name="golesLocal">Goles pronosticados para el equipo local.</param>
        /// <param name="golesVisitante">Goles pronosticados para el equipo visitante.</param>
        public Pronostico(
            string nombreUsuario,
            string local,
            string visitante,
            int golesLocal,
            int golesVisitante)
        {
            this.NombreUsuario = nombreUsuario;
            this.Local = local;
            this.Visitante = visitante;
            this.GolesLocal = golesLocal;
            this.GolesVisitante = golesVisitante;
            this.Puntos = 0;
        }

        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la selección local.
        /// </summary>
        public string Local { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la selección visitante.
        /// </summary>
        public string Visitante { get; set; }

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