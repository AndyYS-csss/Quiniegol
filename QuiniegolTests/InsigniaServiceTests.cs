using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiniegol.Services;
using QuiniegolModels;
using System.Collections.Generic;

namespace QuiniegolTests
{
    [TestClass]
    public class InsigniaServiceTests
    {
        [TestMethod]
        public void ActualizarInsigniasGlobales_DebeAsignarPrimeroYPeor()
        {
            var service = new InsigniaService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Carlos", "Costa Rica", 20),
                new Usuario("Ana", "Brasil", 50),
                new Usuario("Pedro", "México", 30)
            };

            service.ActualizarInsigniasGlobales(usuarios);

            Assert.IsTrue(
                usuarios[1].Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Primero del ranking global"));

            Assert.IsTrue(
                usuarios[0].Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Peor del ranking global"));
        }

        [TestMethod]
        public void ActualizarInsigniasGlobales_NoDebeHacerNadaSiListaEsNull()
        {
            var service = new InsigniaService();

            service.ActualizarInsigniasGlobales(null);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ActualizarInsigniasGlobales_NoDebeHacerNadaSiListaEstaVacia()
        {
            var service = new InsigniaService();

            var usuarios = new List<Usuario>();

            service.ActualizarInsigniasGlobales(usuarios);

            Assert.AreEqual(0, usuarios.Count);
        }

        [TestMethod]
        public void ActualizarInsigniasGlobales_NoDebeDuplicarInsignias()
        {
            var service = new InsigniaService();

            var usuarios = new List<Usuario>
            {
                new Usuario("Ana", "Brasil", 50),
                new Usuario("Carlos", "Costa Rica", 20)
            };

            service.ActualizarInsigniasGlobales(usuarios);
            service.ActualizarInsigniasGlobales(usuarios);

            int cantidad =
                usuarios[0].Insignias.FindAll(
                    insignia =>
                        insignia.Nombre ==
                        "Primero del ranking global").Count;

            Assert.AreEqual(1, cantidad);
        }

        [TestMethod]
        public void ActualizarInsigniasQuiniela_DebeAsignarPrimeroYPeor()
        {
            var service = new InsigniaService();

            var quiniela =
                new Quiniela("Los Campeones", true);

            var primero =
                new Usuario("Ana", "Brasil", 50);

            var segundo =
                new Usuario("Carlos", "Costa Rica", 20);

            quiniela.Integrantes.Add(primero);
            quiniela.Integrantes.Add(segundo);

            service.ActualizarInsigniasQuiniela(quiniela);

            Assert.IsTrue(
                primero.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Primero de quiniela privada"));

            Assert.IsTrue(
                segundo.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Peor jugador de la liga"));
        }

        [TestMethod]
        public void ActualizarInsigniasQuiniela_NoDebeAsignarSiQuinielaEsPublica()
        {
            var service = new InsigniaService();

            var quiniela =
                new Quiniela("Quiniela Pública", false);

            var usuario =
                new Usuario("Ana", "Brasil", 50);

            quiniela.Integrantes.Add(usuario);

            service.ActualizarInsigniasQuiniela(quiniela);

            Assert.IsFalse(
                usuario.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Primero de quiniela privada"));
        }

        [TestMethod]
        public void ActualizarInsigniasQuiniela_NoDebeHacerNadaSiEsNull()
        {
            var service = new InsigniaService();

            service.ActualizarInsigniasQuiniela(null);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ActualizarInsigniaReyDeLosEmpates_DebeAsignarInsignia()
        {
            var service = new InsigniaService();

            var usuario =
                new Usuario("Ana", "Brasil", 20);

            usuario.Pronosticos.Add(
                new Pronostico(
                    "Ana",
                    "Costa Rica",
                    "Brasil",
                    1,
                    1)
                {
                    Puntos = 2
                });

            usuario.Pronosticos.Add(
                new Pronostico(
                    "Ana",
                    "México",
                    "Estados Unidos",
                    2,
                    2)
                {
                    Puntos = 2
                });

            var usuarios =
                new List<Usuario>
                {
                    usuario
                };

            service.ActualizarInsigniaReyDeLosEmpates(usuarios);

            Assert.IsTrue(
                usuario.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Rey de los empates"));
        }

        [TestMethod]
        public void ActualizarInsigniaReyDeLosEmpates_NoDebeAsignarSiNoHayEmpates()
        {
            var service = new InsigniaService();

            var usuario =
                new Usuario("Ana", "Brasil", 20);

            usuario.Pronosticos.Add(
                new Pronostico(
                    "Ana",
                    "Costa Rica",
                    "Brasil",
                    2,
                    1)
                {
                    Puntos = 2
                });

            var usuarios =
                new List<Usuario>
                {
                    usuario
                };

            service.ActualizarInsigniaReyDeLosEmpates(usuarios);

            Assert.IsFalse(
                usuario.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Rey de los empates"));
        }

        [TestMethod]
        public void ActualizarInsigniaRacha_DebeAsignarDespuesDeMasDeDiezAciertos()
        {
            var service = new InsigniaService();

            var usuario =
                new Usuario("Ana", "Brasil", 50);

            for (int i = 0; i < 11; i++)
            {
                usuario.Pronosticos.Add(
                    new Pronostico(
                        "Ana",
                        "Costa Rica",
                        "Brasil",
                        1,
                        1)
                    {
                        Puntos = 2
                    });
            }

            var usuarios =
                new List<Usuario>
                {
                    usuario
                };

            service.ActualizarInsigniaRacha(usuarios);

            Assert.IsTrue(
                usuario.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Racha de más de 10 aciertos"));
        }

        [TestMethod]
        public void ActualizarInsigniaRacha_NoDebeAsignarConDiezAciertos()
        {
            var service = new InsigniaService();

            var usuario =
                new Usuario("Ana", "Brasil", 50);

            for (int i = 0; i < 10; i++)
            {
                usuario.Pronosticos.Add(
                    new Pronostico(
                        "Ana",
                        "Costa Rica",
                        "Brasil",
                        1,
                        1)
                    {
                        Puntos = 2
                    });
            }

            var usuarios =
                new List<Usuario>
                {
                    usuario
                };

            service.ActualizarInsigniaRacha(usuarios);

            Assert.IsFalse(
                usuario.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Racha de más de 10 aciertos"));
        }

        [TestMethod]
        public void ActualizarTodasLasInsignias_DebeAsignarInsigniasGlobalesYDeRacha()
        {
            var service = new InsigniaService();

            var primero =
                new Usuario("Ana", "Brasil", 50);

            var peor =
                new Usuario("Carlos", "Costa Rica", 10);

            for (int i = 0; i < 11; i++)
            {
                primero.Pronosticos.Add(
                    new Pronostico(
                        "Ana",
                        "Costa Rica",
                        "Brasil",
                        1,
                        1)
                    {
                        Puntos = 2
                    });
            }

            var usuarios =
                new List<Usuario>
                {
                    primero,
                    peor
                };

            var quinielas =
                new List<Quiniela>();

            service.ActualizarTodasLasInsignias(
                usuarios,
                quinielas);

            Assert.IsTrue(
                primero.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Primero del ranking global"));

            Assert.IsTrue(
                primero.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Racha de más de 10 aciertos"));

            Assert.IsTrue(
                peor.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Peor del ranking global"));
        }

        [TestMethod]
        public void ActualizarTodasLasInsignias_DebeProcesarQuinielaPrivada()
        {
            var service = new InsigniaService();

            var primero =
                new Usuario("Ana", "Brasil", 50);

            var segundo =
                new Usuario("Carlos", "Costa Rica", 10);

            var quiniela =
                new Quiniela("Los Campeones", true);

            quiniela.Integrantes.Add(primero);
            quiniela.Integrantes.Add(segundo);

            var usuarios =
                new List<Usuario>
                {
                    primero,
                    segundo
                };

            var quinielas =
                new List<Quiniela>
                {
                    quiniela
                };

            service.ActualizarTodasLasInsignias(
                usuarios,
                quinielas);

            Assert.IsTrue(
                primero.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Primero de quiniela privada"));

            Assert.IsTrue(
                segundo.Insignias.Exists(
                    insignia =>
                        insignia.Nombre ==
                        "Peor jugador de la liga"));
        }
    }
}