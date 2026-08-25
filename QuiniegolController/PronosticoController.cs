using QuiniegolController.Abstractions;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolController
{
    /// <summary>
    /// Clase encargada de las operaciones de los pronósticos.
    /// </summary>
    public class PronosticoController
    {
        private IDataHandler<Pronostico> DataHandler { get; set; }

        /// <summary>
        /// Obtiene los pronósticos cargados.
        /// </summary>
        public List<Pronostico> Pronosticos { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase PronosticoController.
        /// </summary>
        /// <param name="dataHandler">Manejador de datos.</param>
        public PronosticoController(IDataHandler<Pronostico> dataHandler)
        {
            DataHandler = dataHandler;
            Pronosticos = new List<Pronostico>();
        }

        /// <summary>
        /// Carga los pronósticos desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de pronósticos.</returns>
        public List<Pronostico> Load(string fileName)
        {
            var pronosticos = this.DataHandler.Load(fileName);

            if (pronosticos != null && pronosticos.Count > 0)
            {
                this.Pronosticos = pronosticos;
                return pronosticos;
            }

            return new List<Pronostico>();
        }

        /// <summary>
        /// Busca un pronóstico de un usuario para un partido.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario.</param>
        /// <param name="local">Nombre de la selección local.</param>
        /// <param name="visitante">Nombre de la selección visitante.</param>
        /// <returns>Pronóstico encontrado o null.</returns>
        public Pronostico FindPronostico(
            string nombreUsuario,
            string local,
            string visitante)
        {
            if (this.Pronosticos != null && this.Pronosticos.Count > 0)
            {
                return this.Pronosticos.Find(
                    pronostico =>
                        pronostico.NombreUsuario == nombreUsuario &&
                        pronostico.Local == local &&
                        pronostico.Visitante == visitante);
            }

            return null;
        }
    }
}