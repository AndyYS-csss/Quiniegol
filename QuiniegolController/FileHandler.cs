using QuiniegolController.Abstractions;
using QuiniegolModels;
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
        public List<T> Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) ||
                !File.Exists(fileName))
            {
                return null;
            }

            var data = new List<T>();
            var lines = File.ReadAllLines(fileName);

            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                var lineElement = lines[i].Split(',');

                // ==========================================
                // CARGAR PARTIDOS
                // ==========================================

                if (typeof(T) == typeof(Partido))
                {
                    var local = new Seleccion(
                        lineElement[0],
                        lineElement[1]);

                    var visitante = new Seleccion(
                        lineElement[2],
                        lineElement[3]);

                    var fecha = DateTime.Parse(lineElement[4]);

                    var partido = new Partido(
                        local,
                        visitante,
                        fecha);

                    data.Add((T)(object)partido);
                }

                // ==========================================
                // CARGAR PRONÓSTICOS
                // ==========================================

                else if (typeof(T) == typeof(Pronostico))
                {
                    var pronostico = new Pronostico(
                        lineElement[0],
                        lineElement[1],
                        lineElement[2],
                        int.Parse(lineElement[3]),
                        int.Parse(lineElement[4]));

                    pronostico.Puntos =
                        int.Parse(lineElement[5]);

                    data.Add((T)(object)pronostico);
                }

                // ==========================================
                // CARGAR NOTIFICACIONES
                // ==========================================

                else if (typeof(T) == typeof(Notificacion))
                {
                    var mensaje = lineElement[0];
                    var fecha = DateTime.Parse(lineElement[1]);

                    var notificacion = new Notificacion(
                        mensaje,
                        fecha);

                    data.Add((T)(object)notificacion);
                }

                // ==========================================
                // CARGAR USUARIOS
                // ==========================================

                else if (typeof(T) == typeof(Usuario))
                {
                    var usuario = new Usuario(
                        lineElement[0],
                        lineElement[1],
                        int.Parse(lineElement[2]));

                    // Compatibilidad con usuarios del Proyecto 1.
                    usuario.Rol = "Usuario";
                    usuario.Contrasena = "1234";
                    usuario.Activo = true;

                    // Usuarios del Proyecto 2:
                    // Nombre, País, Puntos, Rol, Contraseña, Activo
                    if (lineElement.Length >= 6)
                    {
                        usuario.Rol = lineElement[3];
                        usuario.Contrasena = lineElement[4];
                        usuario.Activo =
                            bool.Parse(lineElement[5]);
                    }

                    data.Add((T)(object)usuario);
                }

                // ==========================================
                // CARGAR OTROS TIPOS
                // ==========================================

                else
                {
                    var constructor =
                        typeof(T).GetConstructors()[0];

                    var parameters =
                        constructor.GetParameters();

                    var arguments =
                        new object[parameters.Length];

                    for (var j = 0;
                         j < parameters.Length;
                         j++)
                    {
                        arguments[j] =
                            Convert.ChangeType(
                                lineElement[j],
                                parameters[j].ParameterType);
                    }

                    var newElement =
                        Activator.CreateInstance(
                            typeof(T),
                            arguments);

                    data.Add((T)newElement);
                }
            }

            return data;
        }


        /// <summary>
        /// Actualiza un elemento en el archivo.
        /// </summary>
        public bool Update(
            string fileName,
            T element)
        {
            if (string.IsNullOrEmpty(fileName) ||
                element == null ||
                !File.Exists(fileName))
            {
                return false;
            }

            var lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                return false;
            }

            bool actualizado = false;


            // ==========================================
            // ACTUALIZAR PARTIDO
            // ==========================================

            if (typeof(T) == typeof(Partido))
            {
                var partido =
                    (Partido)(object)element;

                for (var i = 1;
                     i < lines.Length;
                     i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        continue;
                    }

                    var campos = lines[i].Split(',');

                    if (campos.Length < 5)
                    {
                        continue;
                    }

                    bool mismoPartido =
                        campos[0] == partido.Local.Nombre &&
                        campos[2] == partido.Visitante.Nombre;

                    if (mismoPartido)
                    {
                        lines[i] = string.Format(
                            "{0},{1},{2},{3},{4}",
                            partido.Local.Nombre,
                            partido.Local.Grupo,
                            partido.Visitante.Nombre,
                            partido.Visitante.Grupo,
                            partido.Fecha.ToString(
                                "yyyy-MM-dd HH:mm:ss"));

                        actualizado = true;
                        break;
                    }
                }
            }


            // ==========================================
            // ACTUALIZAR USUARIO
            // ==========================================

            else if (typeof(T) == typeof(Usuario))
            {
                var usuario =
                    (Usuario)(object)element;

                for (var i = 1;
                     i < lines.Length;
                     i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        continue;
                    }

                    var campos = lines[i].Split(',');

                    if (campos.Length < 3)
                    {
                        continue;
                    }

                    if (campos[0] == usuario.Nombre)
                    {
                        lines[i] = string.Format(
                            "{0},{1},{2},{3},{4},{5}",
                            usuario.Nombre,
                            usuario.PaisPreferido,
                            usuario.Puntos,
                            usuario.Rol,
                            usuario.Contrasena,
                            usuario.Activo);

                        actualizado = true;
                        break;
                    }
                }
            }


            // ==========================================
            // ACTUALIZAR PRONÓSTICO
            // ==========================================

            else if (typeof(T) == typeof(Pronostico))
            {
                var pronostico =
                    (Pronostico)(object)element;

                for (var i = 1;
                     i < lines.Length;
                     i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        continue;
                    }

                    var campos = lines[i].Split(',');

                    if (campos.Length < 6)
                    {
                        continue;
                    }

                    bool mismoPronostico =
                        campos[0] ==
                            pronostico.NombreUsuario &&
                        campos[1] ==
                            pronostico.Local &&
                        campos[2] ==
                            pronostico.Visitante;

                    if (mismoPronostico)
                    {
                        lines[i] = string.Format(
                            "{0},{1},{2},{3},{4},{5}",
                            pronostico.NombreUsuario,
                            pronostico.Local,
                            pronostico.Visitante,
                            pronostico.GolesLocal,
                            pronostico.GolesVisitante,
                            pronostico.Puntos);

                        actualizado = true;
                        break;
                    }
                }
            }


            // ==========================================
            // ACTUALIZAR NOTIFICACIÓN
            // ==========================================

            else if (typeof(T) == typeof(Notificacion))
            {
                var notificacion =
                    (Notificacion)(object)element;

                for (var i = 1;
                     i < lines.Length;
                     i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        continue;
                    }

                    var campos = lines[i].Split(',');

                    if (campos.Length < 2)
                    {
                        continue;
                    }

                    if (campos[0] ==
                        notificacion.Mensaje)
                    {
                        lines[i] = string.Format(
                            "{0},{1}",
                            notificacion.Mensaje,
                            notificacion.Fecha);

                        actualizado = true;
                        break;
                    }
                }
            }


            if (!actualizado)
            {
                return false;
            }

            File.WriteAllLines(
                fileName,
                lines);

            return true;
        }


        /// <summary>
        /// Elimina un elemento del archivo.
        /// </summary>
        public bool Remove(
            string fileName,
            T element)
        {
            // No eliminamos físicamente usuarios.
            // Para el Proyecto 2 utilizaremos Activo = false.
            return false;
        }


        /// <summary>
        /// Crea un elemento en el archivo.
        /// </summary>
        public bool Create(
            string fileName,
            T element)
        {
            if (string.IsNullOrEmpty(fileName) ||
                element == null)
            {
                return false;
            }


            // ==========================================
            // CREAR USUARIO
            // ==========================================

            if (typeof(T) == typeof(Usuario))
            {
                var usuario =
                    (Usuario)(object)element;

                var linea = string.Format(
                    "{0},{1},{2},{3},{4},{5}",
                    usuario.Nombre,
                    usuario.PaisPreferido,
                    usuario.Puntos,
                    usuario.Rol,
                    usuario.Contrasena,
                    usuario.Activo);

                File.AppendAllText(
                    fileName,
                    Environment.NewLine + linea);

                return true;
            }


            // ==========================================
            // CREAR PRONÓSTICO
            // ==========================================

            if (typeof(T) == typeof(Pronostico))
            {
                var pronostico =
                    (Pronostico)(object)element;

                var linea = string.Format(
                    "{0},{1},{2},{3},{4},{5}",
                    pronostico.NombreUsuario,
                    pronostico.Local,
                    pronostico.Visitante,
                    pronostico.GolesLocal,
                    pronostico.GolesVisitante,
                    pronostico.Puntos);

                File.AppendAllText(
                    fileName,
                    Environment.NewLine + linea);

                return true;
            }


            // ==========================================
            // CREAR QUINIELA
            // ==========================================

            if (typeof(T) == typeof(Quiniela))
            {
                var quiniela =
                    (Quiniela)(object)element;

                var linea = string.Format(
                    "{0},{1}",
                    quiniela.Nombre,
                    quiniela.EsPrivada);

                File.AppendAllText(
                    fileName,
                    Environment.NewLine + linea);

                return true;
            }


            // ==========================================
            // CREAR NOTIFICACIÓN
            // ==========================================

            if (typeof(T) == typeof(Notificacion))
            {
                var notificacion =
                    (Notificacion)(object)element;

                var linea = string.Format(
                    "{0},{1}",
                    notificacion.Mensaje,
                    notificacion.Fecha);

                File.AppendAllText(
                    fileName,
                    Environment.NewLine + linea);

                return true;
            }

            return false;
        }
    }
}