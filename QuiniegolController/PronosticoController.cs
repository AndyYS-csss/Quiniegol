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
        /// <param name="dataHandler">
        /// Manejador de datos.
        /// </param>
        public PronosticoController(
            IDataHandler<Pronostico> dataHandler)
        {
            DataHandler = dataHandler;
            Pronosticos = new List<Pronostico>();
        }

        /// <summary>
        /// Carga los pronósticos desde el archivo indicado.
        /// </summary>
        /// <param name="fileName">
        /// Nombre del archivo.
        /// </param>
        /// <returns>
        /// Lista de pronósticos.
        /// </returns>
        public List<Pronostico> Load(string fileName)
        {
            var pronosticos =
                this.DataHandler.Load(fileName);

            if (pronosticos != null &&
                pronosticos.Count > 0)
            {
                this.Pronosticos = pronosticos;
                return pronosticos;
            }

            return new List<Pronostico>();
        }

        /// <summary>
        /// Busca un pronóstico de un usuario para un partido.
        /// </summary>
        /// <param name="nombreUsuario">
        /// Nombre del usuario.
        /// </param>
        /// <param name="local">
        /// Nombre de la selección local.
        /// </param>
        /// <param name="visitante">
        /// Nombre de la selección visitante.
        /// </param>
        /// <returns>
        /// Pronóstico encontrado o null.
        /// </returns>
        public Pronostico FindPronostico(
            string nombreUsuario,
            string local,
            string visitante)
        {
            if (this.Pronosticos != null &&
                this.Pronosticos.Count > 0)
            {
                return this.Pronosticos.Find(
                    pronostico =>
                        pronostico.NombreUsuario == nombreUsuario &&
                        pronostico.Local == local &&
                        pronostico.Visitante == visitante);
            }

            return null;
        }

        /// <summary>
        /// Registra un nuevo pronóstico.
        /// </summary>
        /// <param name="fileName">
        /// Archivo donde se guardará el pronóstico.
        /// </param>
        /// <param name="nombreUsuario">
        /// Nombre del usuario.
        /// </param>
        /// <param name="partido">
        /// Partido para el cual se realiza el pronóstico.
        /// </param>
        /// <param name="golesLocal">
        /// Goles pronosticados para el equipo local.
        /// </param>
        /// <param name="golesVisitante">
        /// Goles pronosticados para el equipo visitante.
        /// </param>
        /// <param name="fechaSistema">
        /// Fecha y hora simulada del sistema.
        /// </param>
        /// <returns>
        /// True si el pronóstico fue registrado correctamente.
        /// </returns>
        public bool RegisterPronostico(
            string fileName,
            string nombreUsuario,
            Partido partido,
            int golesLocal,
            int golesVisitante,
            System.DateTime fechaSistema)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) ||
                partido == null)
            {
                return false;
            }

            if (golesLocal < 0 ||
                golesVisitante < 0)
            {
                return false;
            }

            // No permite pronosticar cuando el partido
            // ya comenzó.
            if (!partido.AceptaPronosticos(fechaSistema))
            {
                return false;
            }

            // No permite que un usuario registre
            // más de un pronóstico para el mismo partido.
            if (this.FindPronostico(
                    nombreUsuario,
                    partido.Local.Nombre,
                    partido.Visitante.Nombre) != null)
            {
                return false;
            }

            var pronostico = new Pronostico(
                nombreUsuario,
                partido.Local.Nombre,
                partido.Visitante.Nombre,
                golesLocal,
                golesVisitante);

            pronostico.Puntos = 0;

            var created =
                this.DataHandler.Create(
                    fileName,
                    pronostico);

            if (!created)
            {
                return false;
            }

            this.Pronosticos.Add(pronostico);

            return true;
        }

        /// <summary>
        /// Actualiza los puntos obtenidos por un pronóstico.
        /// </summary>
        /// <param name="fileName">
        /// Archivo donde está guardado el pronóstico.
        /// </param>
        /// <param name="nombreUsuario">
        /// Nombre del usuario.
        /// </param>
        /// <param name="local">
        /// Nombre de la selección local.
        /// </param>
        /// <param name="visitante">
        /// Nombre de la selección visitante.
        /// </param>
        /// <param name="puntos">
        /// Nueva cantidad de puntos.
        /// </param>
        /// <returns>
        /// True si los puntos fueron actualizados correctamente.
        /// </returns>
        public bool UpdatePoints(
            string fileName,
            string nombreUsuario,
            string local,
            string visitante,
            int puntos)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) ||
                string.IsNullOrWhiteSpace(local) ||
                string.IsNullOrWhiteSpace(visitante))
            {
                return false;
            }

            if (puntos < 0)
            {
                return false;
            }

            var pronostico = this.FindPronostico(
                nombreUsuario,
                local,
                visitante);

            if (pronostico == null)
            {
                return false;
            }

            pronostico.Puntos = puntos;

            return this.DataHandler.Update(
                fileName,
                pronostico);
        }
    }
}