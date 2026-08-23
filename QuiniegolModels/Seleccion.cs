using System;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa una selección nacional dentro del sistema.
    /// </summary>
    public class Seleccion
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Seleccion.
        /// </summary>
        /// <param name="nombre">Nombre de la selección.</param>
        /// <param name="grupo">Grupo al que pertenece la selección.</param>
        public Seleccion(string nombre, string grupo)
        {
            this.Nombre = nombre;
            this.Grupo = grupo;
        }

        /// <summary>
        /// Obtiene o establece el nombre de la selección.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el grupo de la selección.
        /// </summary>
        public string Grupo { get; set; }
    }
}