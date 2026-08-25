using QuiniegolModels;
using System.Collections.Generic;
using System.Linq;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las insignias
    /// obtenidas por los usuarios.
    /// </summary>
    public class InsigniaService
    {
        private const string PrimeroRankingGlobal =
            "Primero del ranking global";

        private const string PrimeroQuinielaPrivada =
            "Primero de quiniela privada";

        private const string ReyDeLosEmpates =
            "Rey de los empates";

        private const string RachaDeMasDeDiez =
            "Racha de más de 10 aciertos";

        private const string PeorJugadorLiga =
            "Peor jugador de la liga";

        private const string PeorRankingGlobal =
            "Peor del ranking global";

        /// <summary>
        /// Actualiza las insignias correspondientes
        /// al ranking global.
        /// </summary>
        /// <param name="usuarios">
        /// Usuarios participantes del ranking global.
        /// </param>
        public void ActualizarInsigniasGlobales(
            List<Usuario> usuarios)
        {
            if (usuarios == null ||
                usuarios.Count == 0)
            {
                return;
            }

            Usuario primero =
                usuarios
                    .OrderByDescending(
                        usuario => usuario.Puntos)
                    .ThenBy(
                        usuario => usuario.Nombre)
                    .First();

            Usuario peor =
                usuarios
                    .OrderBy(
                        usuario => usuario.Puntos)
                    .ThenBy(
                        usuario => usuario.Nombre)
                    .First();

            AgregarInsignia(
                primero,
                PrimeroRankingGlobal,
                "Usuario con mayor cantidad de puntos del ranking global.");

            AgregarInsignia(
                peor,
                PeorRankingGlobal,
                "Usuario con menor cantidad de puntos del ranking global.");
        }

        /// <summary>
        /// Actualiza las insignias de una quiniela privada.
        /// </summary>
        /// <param name="quiniela">
        /// Quiniela privada que será evaluada.
        /// </param>
        public void ActualizarInsigniasQuiniela(
            Quiniela quiniela)
        {
            if (quiniela == null ||
                !quiniela.EsPrivada ||
                quiniela.Integrantes == null ||
                quiniela.Integrantes.Count == 0)
            {
                return;
            }

            Usuario primero =
                quiniela.Integrantes
                    .OrderByDescending(
                        usuario => usuario.Puntos)
                    .ThenBy(
                        usuario => usuario.Nombre)
                    .First();

            Usuario peor =
                quiniela.Integrantes
                    .OrderBy(
                        usuario => usuario.Puntos)
                    .ThenBy(
                        usuario => usuario.Nombre)
                    .First();

            AgregarInsignia(
                primero,
                PrimeroQuinielaPrivada,
                "Usuario con mayor cantidad de puntos en la quiniela privada.");

            AgregarInsignia(
                peor,
                PeorJugadorLiga,
                "Usuario con menor cantidad de puntos en la quiniela.");
        }

        /// <summary>
        /// Asigna la insignia de rey de los empates.
        /// </summary>
        /// <param name="usuarios">
        /// Usuarios que serán evaluados.
        /// </param>
        public void ActualizarInsigniaReyDeLosEmpates(
            List<Usuario> usuarios)
        {
            if (usuarios == null ||
                usuarios.Count == 0)
            {
                return;
            }

            Usuario usuarioConMasEmpates =
                usuarios
                    .OrderByDescending(
                        usuario => ContarEmpatesAcertados(usuario))
                    .ThenBy(
                        usuario => usuario.Nombre)
                    .First();

            int cantidadEmpates =
                ContarEmpatesAcertados(
                    usuarioConMasEmpates);

            if (cantidadEmpates == 0)
            {
                return;
            }

            AgregarInsignia(
                usuarioConMasEmpates,
                ReyDeLosEmpates,
                "Usuario con mayor cantidad de empates acertados.");
        }

        /// <summary>
        /// Actualiza la insignia de racha de más de diez aciertos.
        /// </summary>
        /// <param name="usuarios">
        /// Usuarios que serán evaluados.
        /// </param>
        public void ActualizarInsigniaRacha(
            List<Usuario> usuarios)
        {
            if (usuarios == null ||
                usuarios.Count == 0)
            {
                return;
            }

            foreach (Usuario usuario in usuarios)
            {
                if (TieneRachaMayorADiez(usuario))
                {
                    AgregarInsignia(
                        usuario,
                        RachaDeMasDeDiez,
                        "Usuario con una racha de más de 10 aciertos.");
                }
            }
        }

        /// <summary>
        /// Actualiza todas las insignias del sistema.
        /// </summary>
        /// <param name="usuarios">
        /// Usuarios del sistema.
        /// </param>
        /// <param name="quinielas">
        /// Quinielas del sistema.
        /// </param>
        public void ActualizarTodasLasInsignias(
            List<Usuario> usuarios,
            List<Quiniela> quinielas)
        {
            if (usuarios == null ||
                usuarios.Count == 0)
            {
                return;
            }

            ActualizarInsigniasGlobales(
                usuarios);

            ActualizarInsigniaReyDeLosEmpates(
                usuarios);

            ActualizarInsigniaRacha(
                usuarios);

            if (quinielas == null)
            {
                return;
            }

            foreach (Quiniela quiniela in quinielas)
            {
                ActualizarInsigniasQuiniela(
                    quiniela);
            }
        }

        /// <summary>
        /// Cuenta los empates acertados por un usuario.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que será evaluado.
        /// </param>
        /// <returns>
        /// Cantidad de empates acertados.
        /// </returns>
        private int ContarEmpatesAcertados(
            Usuario usuario)
        {
            if (usuario == null ||
                usuario.Pronosticos == null)
            {
                return 0;
            }

            return usuario.Pronosticos.Count(
                pronostico =>
                    pronostico != null &&
                    pronostico.Puntos > 0 &&
                    pronostico.GolesLocal ==
                    pronostico.GolesVisitante);
        }

        /// <summary>
        /// Determina si un usuario tiene una racha
        /// consecutiva superior a diez aciertos.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que será evaluado.
        /// </param>
        /// <returns>
        /// True si tiene más de diez aciertos consecutivos.
        /// </returns>
        private bool TieneRachaMayorADiez(
            Usuario usuario)
        {
            if (usuario == null ||
                usuario.Pronosticos == null ||
                usuario.Pronosticos.Count == 0)
            {
                return false;
            }

            int rachaActual = 0;

            foreach (Pronostico pronostico
                in usuario.Pronosticos)
            {
                if (pronostico != null &&
                    pronostico.Puntos > 0)
                {
                    rachaActual++;

                    if (rachaActual > 10)
                    {
                        return true;
                    }
                }
                else
                {
                    rachaActual = 0;
                }
            }

            return false;
        }

        /// <summary>
        /// Agrega una insignia al usuario evitando duplicados.
        /// </summary>
        /// <param name="usuario">
        /// Usuario que recibirá la insignia.
        /// </param>
        /// <param name="nombre">
        /// Nombre de la insignia.
        /// </param>
        /// <param name="descripcion">
        /// Descripción de la insignia.
        /// </param>
        private void AgregarInsignia(
            Usuario usuario,
            string nombre,
            string descripcion)
        {
            if (usuario == null)
            {
                return;
            }

            if (usuario.Insignias == null)
            {
                usuario.Insignias =
                    new List<Insignia>();
            }

            bool yaExiste =
                usuario.Insignias.Any(
                    insignia =>
                        insignia != null &&
                        insignia.Nombre == nombre);

            if (yaExiste)
            {
                return;
            }

            usuario.Insignias.Add(
                new Insignia(
                    nombre,
                    descripcion));
        }
    }
}