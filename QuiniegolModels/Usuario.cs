using System.Collections.Generic;

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
        /// <param name="nombre">Nombre completo del usuario.</param>
        /// <param name="paisPreferido">País preferido del usuario.</param>
        /// <param name="puntos">Puntos acumulados por el usuario.</param>
        public Usuario(
            string nombre,
            string paisPreferido,
            int puntos)
        {
            this.Nombre = nombre;
            this.PaisPreferido = paisPreferido;
            this.Puntos = puntos;

            // Los usuarios existentes del Proyecto 1
            // continúan siendo usuarios normales.
            this.Rol = "Usuario";

            // Valores iniciales para la segunda iteración.
            this.Contrasena = string.Empty;
            this.Activo = true;

            this.Pronosticos = new List<Pronostico>();
            this.Quinielas = new List<Quiniela>();
            this.Insignias = new List<Insignia>();
        }

        /// <summary>
        /// Obtiene o establece el nombre completo del usuario.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el país preferido.
        /// </summary>
        public string PaisPreferido { get; set; }

        /// <summary>
        /// Obtiene o establece los puntos acumulados.
        /// </summary>
        public int Puntos { get; set; }

        /// <summary>
        /// Obtiene o establece el rol del usuario.
        /// Puede ser Usuario o Administrador.
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; }

        /// <summary>
        /// Obtiene o establece si el usuario se encuentra activo.
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Obtiene los pronósticos realizados por el usuario.
        /// </summary>
        public List<Pronostico> Pronosticos { get; set; }

        /// <summary>
        /// Obtiene las quinielas a las que pertenece el usuario.
        /// </summary>
        public List<Quiniela> Quinielas { get; set; }

        /// <summary>
        /// Obtiene las insignias obtenidas por el usuario.
        /// </summary>
        public List<Insignia> Insignias { get; set; }
    }
}