using System;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa una insignia que puede obtener un usuario.
    /// </summary>
    public class Insignia
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Insignia.
        /// </summary>
        /// <param name="nombre">Nombre de la insignia.</param>
        /// <param name="descripcion">Descripción de la insignia.</param>
        public Insignia(string nombre, string descripcion)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
        }

        /// <summary>
        /// Obtiene o establece el nombre de la insignia.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción de la insignia.
        /// </summary>
        public string Descripcion { get; set; }
    }
}