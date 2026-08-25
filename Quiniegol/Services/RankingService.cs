using QuiniegolModels;
using System.Collections.Generic;
using System.Linq;

namespace Quiniegol.Services
{
    /// <summary>
    /// Servicio encargado de gestionar los rankings
    /// de los usuarios del sistema.
    /// </summary>
    public class RankingService
    {
        /// <summary>
        /// Obtiene el ranking global de usuarios,
        /// ordenado de mayor a menor cantidad de puntos.
        /// </summary>
        /// <param name="usuarios">
        /// Lista de usuarios que participarán en el ranking.
        /// </param>
        /// <returns>
        /// Lista de usuarios ordenada por puntos.
        /// </returns>
        public List<Usuario> ObtenerRankingGlobal(
            List<Usuario> usuarios)
        {
            if (usuarios == null)
            {
                return new List<Usuario>();
            }

            return usuarios
                .OrderByDescending(usuario => usuario.Puntos)
                .ThenBy(usuario => usuario.Nombre)
                .ToList();
        }

        /// <summary>
        /// Obtiene el ranking de los integrantes
        /// de una quiniela privada.
        /// </summary>
        /// <param name="quiniela">
        /// Quiniela privada que se desea consultar.
        /// </param>
        /// <returns>
        /// Lista de integrantes ordenada por puntos.
        /// </returns>
        public List<Usuario> ObtenerRankingPrivado(
            Quiniela quiniela)
        {
            if (quiniela == null)
            {
                return new List<Usuario>();
            }

            if (!quiniela.EsPrivada)
            {
                return new List<Usuario>();
            }

            if (quiniela.Integrantes == null)
            {
                return new List<Usuario>();
            }

            return quiniela.Integrantes
                .OrderByDescending(usuario => usuario.Puntos)
                .ThenBy(usuario => usuario.Nombre)
                .ToList();
        }

        /// <summary>
        /// Obtiene la posición de un usuario
        /// dentro del ranking global.
        /// </summary>
        /// <param name="usuarios">
        /// Lista de usuarios.
        /// </param>
        /// <param name="nombreUsuario">
        /// Nombre del usuario que se desea consultar.
        /// </param>
        /// <returns>
        /// Posición del usuario comenzando en 1.
        /// Retorna 0 si el usuario no existe.
        /// </returns>
        public int ObtenerPosicionGlobal(
            List<Usuario> usuarios,
            string nombreUsuario)
        {
            if (usuarios == null ||
                string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return 0;
            }

            var ranking =
                ObtenerRankingGlobal(usuarios);

            var posicion =
                ranking.FindIndex(
                    usuario =>
                        usuario.Nombre == nombreUsuario);

            if (posicion == -1)
            {
                return 0;
            }

            return posicion + 1;
        }

        /// <summary>
        /// Obtiene la posición de un usuario
        /// dentro de una quiniela privada.
        /// </summary>
        /// <param name="quiniela">
        /// Quiniela privada.
        /// </param>
        /// <param name="nombreUsuario">
        /// Nombre del usuario.
        /// </param>
        /// <returns>
        /// Posición del usuario comenzando en 1.
        /// Retorna 0 si no existe.
        /// </returns>
        public int ObtenerPosicionPrivada(
            Quiniela quiniela,
            string nombreUsuario)
        {
            if (quiniela == null ||
                string.IsNullOrWhiteSpace(nombreUsuario))
            {
                return 0;
            }

            var ranking =
                ObtenerRankingPrivado(quiniela);

            var posicion =
                ranking.FindIndex(
                    usuario =>
                        usuario.Nombre == nombreUsuario);

            if (posicion == -1)
            {
                return 0;
            }

            return posicion + 1;
        }
    }
}