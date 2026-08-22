using System.Collections.Generic;

namespace QuiniegolController.Abstractions
{
    /// <summary>
    /// Contrato para las operaciones de datos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataHandler<T>
        where T : class
    {
        /// <summary>
        /// Carga los datos del archivo especificado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>Una lista con los elementos del archivo.</returns>
        List<T> Load(string fileName);

        /// <summary>
        /// Actualiza el elemento especificado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <param name="element">Elemento que se actualizará.</param>
        /// <returns>true si se actualizó, de lo contrario false.</returns>
        bool Update(string fileName, T element);

        /// <summary>
        /// Elimina el elemento especificado.
        /// </summary>
        /// <param name="filename">Nombre del archivo.</param>
        /// <param name="element">Elemento que se eliminará.</param>
        /// <returns>true si se eliminó, de lo contrario false.</returns>
        bool Remove(string filename, T element);

        /// <summary>
        /// Crea el elemento especificado.
        /// </summary>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <param name="element">Elemento que se creará.</param>
        /// <returns>true si se creó, de lo contrario false.</returns>
        bool Create(string fileName, T element);
    }
}