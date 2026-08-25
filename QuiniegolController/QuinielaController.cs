using QuiniegolController.Abstractions;
using QuiniegolModels;
using System.Collections.Generic;
using System.Linq;

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
        public QuinielaController(
            IDataHandler<Quiniela> dataHandler)
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
            var quinielas =
                this.DataHandler.Load(fileName);

            if (quinielas != null)
            {
                this.Quinielas = quinielas;
                return quinielas;
            }

            return new List<Quiniela>();
        }

        /// <summary>
        /// Busca una quiniela por su nombre.
        /// </summary>
        /// <param name="nombre">
        /// Nombre de la quiniela.
        /// </param>
        /// <returns>
        /// Quiniela encontrada o null.
        /// </returns>
        public Quiniela FindQuiniela(string nombre)
        {
            if (this.Quinielas == null ||
                this.Quinielas.Count == 0)
            {
                return null;
            }

            return this.Quinielas.Find(
                quiniela =>
                    quiniela.Nombre == nombre);
        }

        /// <summary>
        /// Crea una nueva quiniela.
        /// </summary>
        /// <param name="fileName">
        /// Archivo donde se guardará la quiniela.
        /// </param>
        /// <param name="nombre">
        /// Nombre de la quiniela.
        /// </param>
        /// <param name="esPrivada">
        /// Indica si la quiniela es privada.
        /// </param>
        /// <returns>
        /// True si la quiniela fue creada correctamente.
        /// </returns>
        public bool CreateQuiniela(
            string fileName,
            string nombre,
            bool esPrivada)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return false;
            }

            if (this.FindQuiniela(nombre) != null)
            {
                return false;
            }

            var quiniela =
                new Quiniela(nombre, esPrivada);

            var created =
                this.DataHandler.Create(
                    fileName,
                    quiniela);

            if (!created)
            {
                return false;
            }

            this.Quinielas.Add(quiniela);

            return true;
        }

        /// <summary>
        /// Agrega un usuario como integrante de una quiniela.
        /// </summary>
        /// <param name="nombreQuiniela">
        /// Nombre de la quiniela.
        /// </param>
        /// <param name="usuario">
        /// Usuario que se desea agregar.
        /// </param>
        /// <returns>
        /// True si el usuario fue agregado correctamente.
        /// </returns>
        public bool AddIntegrante(
            string nombreQuiniela,
            Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(nombreQuiniela) ||
                usuario == null)
            {
                return false;
            }

            var quiniela =
                this.FindQuiniela(nombreQuiniela);

            if (quiniela == null)
            {
                return false;
            }

            if (this.EsIntegrante(
                    nombreQuiniela,
                    usuario.Nombre))
            {
                return false;
            }

            quiniela.Integrantes.Add(usuario);

            return true;
        }

        /// <summary>
        /// Comprueba si un usuario pertenece a una quiniela.
        /// </summary>
        /// <param name="nombreQuiniela">
        /// Nombre de la quiniela.
        /// </param>
        /// <param name="nombreUsuario">
        /// Nombre del usuario.
        /// </param>
        /// <returns>
        /// True si el usuario pertenece a la quiniela.
        /// </returns>
        public bool EsIntegrante(
            string nombreQuiniela,
            string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreQuiniela) ||
                string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return false;
            }

            var quiniela =
                this.FindQuiniela(nombreQuiniela);

            if (quiniela == null ||
                quiniela.Integrantes == null)
            {
                return false;
            }

            return quiniela.Integrantes.Any(
                usuario =>
                    usuario.Nombre == nombreUsuario);
        }

        /// <summary>
        /// Obtiene los integrantes de una quiniela
        /// ordenados por sus puntos de mayor a menor.
        /// </summary>
        /// <param name="nombreQuiniela">
        /// Nombre de la quiniela.
        /// </param>
        /// <returns>
        /// Lista de integrantes ordenada por puntos.
        /// </returns>
        public List<Usuario> ObtenerPosiciones(
            string nombreQuiniela)
        {
            var quiniela =
                this.FindQuiniela(nombreQuiniela);

            if (quiniela == null ||
                quiniela.Integrantes == null)
            {
                return new List<Usuario>();
            }

            return quiniela.Integrantes
                .OrderByDescending(usuario => usuario.Puntos)
                .ThenBy(usuario => usuario.Nombre)
                .ToList();
        }
    }
}