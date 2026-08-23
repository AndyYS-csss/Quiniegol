using System;
using System.Collections.Generic;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa un partido dentro del sistema.
    /// </summary>
    public class Partido
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Partido.
        /// </summary>
        /// <param name="local">Selección que juega como equipo local.</param>
        /// <param name="visitante">Selección que juega como equipo visitante.</param>
        /// <param name="fecha">Fecha y hora programada para el partido.</param>
        public Partido(Seleccion local, Seleccion visitante, DateTime fecha)
        {
            this.Local = local;
            this.Visitante = visitante;
            this.Fecha = fecha;
            this.GolesLocal = 0;
            this.GolesVisitante = 0;
            this.Finalizado = false;
            this.Anotadores = new List<string>();
        }

        /// <summary>
        /// Obtiene o establece la selección local.
        /// </summary>
        public Seleccion Local { get; set; }

        /// <summary>
        /// Obtiene o establece la selección visitante.
        /// </summary>
        public Seleccion Visitante { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha y hora del partido.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Obtiene o establece los goles anotados por la selección local.
        /// </summary>
        public int GolesLocal { get; set; }

        /// <summary>
        /// Obtiene o establece los goles anotados por la selección visitante.
        /// </summary>
        public int GolesVisitante { get; set; }

        /// <summary>
        /// Obtiene o establece si el partido ya finalizó.
        /// </summary>
        public bool Finalizado { get; set; }

        /// <summary>
        /// Obtiene los nombres de los anotadores del partido.
        /// </summary>
        public List<string> Anotadores { get; set; }

        /// <summary>
        /// Determina si el partido ya inició según la fecha indicada.
        /// </summary>
        /// <param name="fechaSistema">Fecha y hora simulada del sistema.</param>
        /// <returns>True si el partido ya inició; de lo contrario, False.</returns>
        public bool EstaEnCurso(DateTime fechaSistema)
        {
            return fechaSistema >= this.Fecha && !this.Finalizado;
        }

        /// <summary>
        /// Determina si el partido permite realizar pronósticos.
        /// </summary>
        /// <param name="fechaSistema">Fecha y hora simulada del sistema.</param>
        /// <returns>True si todavía se pueden realizar pronósticos.</returns>
        public bool AceptaPronosticos(DateTime fechaSistema)
        {
            return fechaSistema < this.Fecha && !this.Finalizado;
        }
    }
}