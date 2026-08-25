using QuiniegolController;
using QuiniegolModels;
using Quiniegol.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Quiniegol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ==========================================
            // CARGA DE USUARIOS
            // ==========================================

            var dataHandler = new FileHandler<Usuario>();

            var usuarioController =
                new UsuarioController(dataHandler);

            string rutaArchivo = Path.Combine(
                Application.StartupPath,
                "Data",
                "Usuarios.csv");

            List<Usuario> usuarios =
                usuarioController.Load(rutaArchivo);

            MessageBox.Show(
                $"Usuarios cargados: {usuarios.Count}",
                "Quiniegol");


            // ==========================================
            // PUNTO 4 - REGISTRO DE USUARIO
            // ==========================================

            bool usuarioRegistrado =
                usuarioController.RegisterUser(
                    rutaArchivo,
                    "Usuario Prueba",
                    "Costa Rica");

            MessageBox.Show(
                $"¿Usuario registrado correctamente?: " +
                $"{usuarioRegistrado}",
                "Prueba de Registro");


            // ==========================================
            // CARGA DE PARTIDOS
            // ==========================================

            var partidoDataHandler =
                new FileHandler<Partido>();

            var partidoController =
                new PartidoController(
                    partidoDataHandler);

            string rutaPartidos = Path.Combine(
                Application.StartupPath,
                "Data",
                "Partidos.csv");

            List<Partido> partidos =
                partidoController.Load(rutaPartidos);

            MessageBox.Show(
                $"Partidos cargados: {partidos.Count}",
                "Quiniegol");


            // ==========================================
            // CARGA DE PRONÓSTICOS
            // ==========================================

            var pronosticoDataHandler =
                new FileHandler<Pronostico>();

            var pronosticoController =
                new PronosticoController(
                    pronosticoDataHandler);

            string rutaPronosticos = Path.Combine(
                Application.StartupPath,
                "Data",
                "Pronosticos.csv");

            List<Pronostico> pronosticos =
                pronosticoController.Load(
                    rutaPronosticos);

            MessageBox.Show(
                $"Pronósticos cargados: " +
                $"{pronosticos.Count}",
                "Quiniegol");


            // ==========================================
            // PRUEBA DEL RELOJ SIMULADO
            // ==========================================

            var relojService =
                new RelojService();

            DateTime horaActual =
                relojService.ObtenerFechaHoraActual();

            var partidoPrueba =
                new Partido(
                    new Seleccion(
                        "Costa Rica",
                        "CRC"),

                    new Seleccion(
                        "México",
                        "MEX"),

                    horaActual.AddHours(1));

            bool aceptaAntes =
                relojService.AceptaPronosticos(
                    partidoPrueba);

            MessageBox.Show(
                $"Hora actual: " +
                $"{horaActual:dd/MM/yyyy HH:mm}\n" +

                $"Partido: " +
                $"{partidoPrueba.Local.Nombre} " +
                $"vs " +
                $"{partidoPrueba.Visitante.Nombre}\n\n" +

                $"¿Acepta pronósticos antes " +
                $"del partido?: {aceptaAntes}",
                "Prueba del Reloj");


            // ==========================================
            // AVANZAMOS UNA HORA
            // ==========================================

            relojService.AvanzarTiempo(
                TimeSpan.FromHours(1));

            bool aceptaDespues =
                relojService.AceptaPronosticos(
                    partidoPrueba);

            bool estaEnCurso =
                relojService.EstaEnCurso(
                    partidoPrueba);

            MessageBox.Show(
                $"Hora después de avanzar: " +
                $"{relojService.ObtenerFechaHoraActual():dd/MM/yyyy HH:mm}\n\n" +

                $"¿Acepta pronósticos?: " +
                $"{aceptaDespues}\n" +

                $"¿Está en curso?: " +
                $"{estaEnCurso}",
                "Prueba del Reloj");
        }
    }
}