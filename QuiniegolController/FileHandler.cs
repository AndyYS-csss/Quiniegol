using QuiniegolController.Abstractions;
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
                var newElement = Activator.CreateInstance(typeof(T), lineElement);
                data.Add((T)newElement);
            }

            return data;
        }

        /// <summary>
        /// Actualiza un elemento en el archivo.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <param name="element">Elemento que se actualizará.</param>
        /// <returns>True si se actualizó correctamente.</returns>
        public bool Update(string fileName, T element)
        {
            return false;
        }

        /// <summary>
        /// Elimina un elemento del archivo.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <param name="element">Elemento que se eliminará.</param>
        /// <returns>True si se eliminó correctamente.</returns>
        public bool Remove(string fileName, T element)
        {
            return false;
        }

        /// <summary>
        /// Crea un elemento en el archivo.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <param name="element">Elemento que se creará.</param>
        /// <returns>True si se creó correctamente.</returns>
        public bool Create(string fileName, T element)
        {
            return false;
        }
    }
}