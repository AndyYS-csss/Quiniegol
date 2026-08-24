using QuiniegolController.Abstractions;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolController
{
    /// <summary>
    /// Clase encargada de las operaciones de los partidos.
    /// </summary>
    public class PartidoController
    {
        private IDataHandler<Partido> DataHandler { get; set; }

        /// <summary>
        /// Obtiene los partidos cargados.
        /// </summary>
        public List<Partido> Partidos { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase PartidoController.
        /// </summary>
        /// <param name="dataHandler">Manejador de datos.</param>
        public PartidoController(IDataHandler<Partido> dataHandler)
        {
            DataHandler = dataHandler;
            Partidos = new List<Partido>();
     
        }
        /// <summary>
        /// Carga los partidos desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de partidos.</returns>
        public List<Partido> Load(string fileName)
        {
            var partidos = this.DataHandler.Load(fileName);

            if (partidos != null && partidos.Count > 0)
            {
                this.Partidos = partidos;
                return partidos;
            }

            return new List<Partido>();
        }
        /// <summary>
        /// Busca un partido por los nombres de las selecciones.
        /// </summary>
        /// <param name="local">Nombre de la selección local.</param>
        /// <param name="visitante">Nombre de la selección visitante.</param>
        /// <returns>Partido encontrado o null.</returns>
        public Partido FindMatch(string local, string visitante)
        {
            if (this.Partidos != null && this.Partidos.Count > 0)
            {
                return this.Partidos.Find(
                    partido => partido.Local.Nombre == local &&
                               partido.Visitante.Nombre == visitante);
            }

            return null;
        }
        /// <summary>
        /// Actualiza el resultado de un partido.
        /// </summary>
        /// <param name="local">Nombre de la selección local.</param>
        /// <param name="visitante">Nombre de la selección visitante.</param>
        /// <param name="golesLocal">Goles del equipo local.</param>
        /// <param name="golesVisitante">Goles del equipo visitante.</param>
        /// <returns>True si el resultado fue actualizado; de lo contrario, false.</returns>
        public bool UpdateResult(
            string local,
            string visitante,
            int golesLocal,
            int golesVisitante)
        {
            var partido = this.FindMatch(local, visitante);

            if (partido == null)
            {
                return false;
            }

            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;
            partido.Finalizado = true;

            return true;
        }
    }
}