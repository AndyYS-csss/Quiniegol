using QuiniegolController.Abstractions;
using QuiniegolModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuiniegolController
{
    /// <summary>
    /// Maneja las operaciones de datos utilizando archivos.
    /// </summary>
    /// <typeparam name="T">Tipo de dato que se va a manejar.</typeparam>
    public class FileHandler<T> : IDataHandler<T>
        where T : class
    {
        /// <summary>
        /// Carga los elementos desde el archivo.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Lista de elementos.</returns>
        public List<T> Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) ||
                !File.Exists(fileName))
            {
                return null;
            }

            var data = new List<T>();

            var lines = File.ReadAllLines(fileName);

            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                var lineElement = lines[i].Split(',');

                // ==========================================
                // CARGAR PARTIDOS
                // ==========================================

                if (typeof(T) == typeof(Partido))
                {
                    var local = new Seleccion(
                        lineElement[0],
                        lineElement[1]);

                    var visitante = new Seleccion(
                        lineElement[2],
                        lineElement[3]);

                    var fecha =
                        DateTime.Parse(lineElement[4]);

                    var partido = new Partido(
                        local,
                        visitante,
                        fecha);

                    data.Add((T)(object)partido);
                }

                // ==========================================
                // CARGAR PRONÓSTICOS
                // ==========================================

                else if (typeof(T) == typeof(Pronostico))
                {
                    var pronostico = new Pronostico(
                        lineElement[0],
                        lineElement[1],
                        lineElement[2],
                        int.Parse(lineElement[3]),
                        int.Parse(lineElement[4]));

                    pronostico.Puntos =
                        int.Parse(lineElement[5]);

                    data.Add((T)(object)pronostico);
                }

                // ==========================================
                // OTROS TIPOS DE DATOS
                // ==========================================

                else
                {
                    var constructor =
                        typeof(T).GetConstructors()[0];

                    var parameters =
                        constructor.GetParameters();

                    var arguments =
                        new object[parameters.Length];

                    for (var j = 0;
                         j < parameters.Length;
                         j++)
                    {
                        arguments[j] =
                            Convert.ChangeType(
                                lineElement[j],
                                parameters[j].ParameterType);
                    }

                    var newElement =
                        Activator.CreateInstance(
                            typeof(T),
                            arguments);

                    data.Add((T)newElement);
                }
            }

            return data;
        }

        /// <summary>
        /// Actualiza un elemento en el archivo.
        /// </summary>
        /// <param name="fileName">
        /// Nombre del archivo.
        /// </param>
        /// <param name="element">
        /// Elemento que se desea actualizar.
        /// </param>
        /// <returns>
        /// True si se actualizó correctamente.
        /// </returns>
        public bool Update(
            string fileName,
            T element)
        {
            return false;
        }

        /// <summary>
        /// Elimina un elemento del archivo.
        /// </summary>
        /// <param name="fileName">
        /// Nombre del archivo.
        /// </param>
        /// <param name="element">
        /// Elemento que se desea eliminar.
        /// </param>
        /// <returns>
        /// True si se eliminó correctamente.
        /// </returns>
        public bool Remove(
            string fileName,
            T element)
        {
            return false;
        }

        /// <summary>
        /// Crea un elemento en el archivo.
        /// </summary>
        /// <param name="fileName">
        /// Nombre del archivo.
        /// </param>
        /// <param name="element">
        /// Elemento que se desea crear.
        /// </param>
        /// <returns>
        /// True si se creó correctamente.
        /// </returns>
        public bool Create(
            string fileName,
            T element)
        {
            if (string.IsNullOrEmpty(fileName) ||
                element == null)
            {
                return false;
            }

            // ==========================================
            // CREAR USUARIO
            // ==========================================

            if (typeof(T) == typeof(Usuario))
            {
                var usuario =
                    (Usuario)(object)element;

                var linea = string.Format(
                    "{0},{1},{2}",
                    usuario.Nombre,
                    usuario.PaisPreferido,
                    usuario.Puntos);

                File.AppendAllText(
                    fileName,
                    Environment.NewLine + linea);

                return true;
            }

            // ==========================================
            // CREAR PRONÓSTICO
            // ==========================================

            if (typeof(T) == typeof(Pronostico))
            {
                var pronostico =
                    (Pronostico)(object)element;

                var linea = string.Format(
                    "{0},{1},{2},{3},{4},{5}",
                    pronostico.NombreUsuario,
                    pronostico.Local,
                    pronostico.Visitante,
                    pronostico.GolesLocal,
                    pronostico.GolesVisitante,
                    pronostico.Puntos);

                File.AppendAllText(
                    fileName,
                    Environment.NewLine + linea);

                return true;
            }

            return false;
        }
    }
}