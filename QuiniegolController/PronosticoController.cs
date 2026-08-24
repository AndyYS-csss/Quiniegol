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
    }
}