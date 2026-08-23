using System;

namespace QuiniegolModels
{
    /// <summary>
    /// Representa el reloj simulado del sistema.
    /// </summary>
    public class Reloj
    {
        private static Reloj instance;

        private DateTime currentDateTime;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Reloj.
        /// </summary>
        private Reloj()
        {
            this.currentDateTime = DateTime.Now;
        }

        /// <summary>
        /// Obtiene la instancia única del reloj.
        /// </summary>
        public static Reloj Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Reloj();
                }

                return instance;
            }
        }

        /// <summary>
        /// Obtiene la fecha y hora actual simulada del sistema.
        /// </summary>
        public DateTime CurrentDateTime
        {
            get
            {
                return this.currentDateTime;
            }
        }

        /// <summary>
        /// Ocurre cuando cambia la fecha y hora simulada.
        /// </summary>
        public event EventHandler TimeChanged;

        /// <summary>
        /// Avanza el reloj una cantidad determinada de tiempo.
        /// </summary>
        /// <param name="amount">Cantidad de tiempo que se desea avanzar.</param>
        public void AdvanceTime(TimeSpan amount)
        {
            this.currentDateTime += amount;

            this.TimeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}