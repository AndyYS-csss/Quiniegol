using System;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa al usuario de Quiniegol.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase Usuario.
        /// </summary>
        /// <param name="paisPreferido">País preferido del usuario.</param>
        /// <param name="puntos">Puntos acumulados por el usuario.</param>
        public Usuario(string paisPreferido, int puntos)
        {
            this.PaisPreferido = paisPreferido;
            this.Puntos = puntos;
        }

        /// <summary>
        /// Obtiene o establece el país preferido.
        /// </summary>
        public string PaisPreferido { get; set; }

        /// <summary>
        /// Obtiene o establece los puntos acumulados.
        /// </summary>
        public int Puntos { get; set; }
    }
}