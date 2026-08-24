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
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                return null;
            }

            var data = new List<T>();
            var lines = File.ReadAllLines(fileName);

            for (var i = 1; i < lines.Length; i++)
            {
                var lineElement = lines[i].Split(',');

                if (typeof(T) == typeof(Partido))
                {
                    var local = new Seleccion(
                        lineElement[0],
                        lineElement[1]);

                    var visitante = new Seleccion(
                        lineElement[2],
                        lineElement[3]);

                    var fecha = DateTime.Parse(lineElement[4]);

                    var partido = new Partido(
                        local,
                        visitante,
                        fecha);

                    data.Add((T)(object)partido);
                }
                else
                {
                    var constructor = typeof(T).GetConstructors()[0];
                    var parameters = constructor.GetParameters();
                    var arguments = new object[parameters.Length];

                    for (var j = 0; j < parameters.Length; j++)
                    {
                        arguments[j] = Convert.ChangeType(
                            lineElement[j],
                            parameters[j].ParameterType);
                    }

                    var newElement = Activator.CreateInstance(
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
        public bool Update(string fileName, T element)
        {
            return false;
        }

        /// <summary>
        /// Elimina un elemento del archivo.
        /// </summary>
        public bool Remove(string fileName, T element)
        {
            return false;
        }

        /// <summary>
        /// Crea un elemento en el archivo.
        /// </summary>
        public bool Create(string fileName, T element)
        {
            return false;
        }
    }
}