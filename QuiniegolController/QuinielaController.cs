using QuiniegolController.Abstractions;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolController
{
    /// <summary>
    /// Clase encargada de las operaciones de las quinielas.
    /// </summary>
    public class QuinielaController
    {
        private IDataHandler<Quiniela> DataHandler { get; set; }

        /// <summary>
        /// Obtiene las quinielas cargadas.
        /// </summary>
        public List<Quiniela> Quinielas { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase QuinielaController.
        /// </summary>
        /// <param name="dataHandler">Manejador de datos.</param>
        public QuinielaController(IDataHandler<Quiniela> dataHandler)
        {
            DataHandler = dataHandler;
            Quinielas = new List<Quiniela>();
        }

        /// <summary>
        /// Carga las quinielas desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de quinielas.</returns>
        public List<Quiniela> Load(string fileName)
        {
            var quinielas = this.DataHandler.Load(fileName);

            if (quinielas != null && quinielas.Count > 0)
            {
                this.Quinielas = quinielas;
                return quinielas;
            }

            return new List<Quiniela>();
        }

        /// <summary>
        /// Busca una quiniela por su nombre.
        /// </summary>
        /// <param name="nombre">Nombre de la quiniela.</param>
        /// <returns>Quiniela encontrada o null.</returns>
        public Quiniela FindQuiniela(string nombre)
        {
            if (this.Quinielas != null && this.Quinielas.Count > 0)
            {
                return this.Quinielas.Find(
                    quiniela => quiniela.Nombre == nombre);
            }

            return null;
        }
    }
}