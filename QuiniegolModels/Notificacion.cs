using System;

namespace QuiniegolModels
{
    public class Notificacion
    {
        public Notificacion(string mensaje, DateTime fecha)
        {
            this.Mensaje = mensaje;
            this.Fecha = fecha;
        }

        public string Mensaje { get; set; }

        public DateTime Fecha { get; set; }
    }
}