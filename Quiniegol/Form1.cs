using QuiniegolController;
using QuiniegolModels;
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
            var dataHandler = new FileHandler<Usuario>();
            var usuarioController = new UsuarioController(dataHandler);

            string rutaArchivo = Path.Combine(
                Application.StartupPath,
                "Data",
                "Usuarios.csv");

            List<Usuario> usuarios = usuarioController.Load(rutaArchivo);

            MessageBox.Show(
                $"Usuarios cargados: {usuarios.Count}",
                "Quiniegol");

            var partidoDataHandler = new FileHandler<Partido>();
            var partidoController = new PartidoController(partidoDataHandler);

            string rutaPartidos = Path.Combine(
                Application.StartupPath,
                "Data",
                "Partidos.csv");

            List<Partido> partidos = partidoController.Load(rutaPartidos);

            MessageBox.Show(
                $"Partidos cargados: {partidos.Count}",
                "Quiniegol");
        }
    }
}