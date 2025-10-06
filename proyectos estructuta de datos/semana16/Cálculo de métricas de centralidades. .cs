using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace CentralidadGrafos
{
    public class Grafo
    {
        public Dictionary<string, List<string>> ListaAdyacencia { get; private set; }
        public List<string> Nodos { get; private set; }

        public Grafo()
        {
            ListaAdyacencia = new Dictionary<string, List<string>>();
            Nodos = new List<string>();
        }

        public void AgregarArista(string origen, string destino)
        {
            if (!ListaAdyacencia.ContainsKey(origen))
            {
                ListaAdyacencia[origen] = new List<string>();
                if (!Nodos.Contains(origen)) Nodos.Add(origen);
            }
            
            if (!ListaAdyacencia.ContainsKey(destino))
            {
                ListaAdyacencia[destino] = new List<string>();
                if (!Nodos.Contains(destino)) Nodos.Add(destino);
            }

            if (!ListaAdyacencia[origen].Contains(destino))
                ListaAdyacencia[origen].Add(destino);

            if (!ListaAdyacencia[destino].Contains(origen))
                ListaAdyacencia[destino].Add(origen);
        }

        public void CargarEjemploRedSocial()
        {
            // Red social de ejemplo: 10 usuarios con conexiones
            AgregarArista("Ana", "Carlos");
            AgregarArista("Ana", "Maria");
            AgregarArista("Carlos", "Pedro");
            AgregarArista("Maria", "Laura");
            AgregarArista("Pedro", "Javier");
            AgregarArista("Laura", "Sofia");
            AgregarArista("Javier", "Diego");
            AgregarArista("Sofia", "Elena");
            AgregarArista("Diego", "Miguel");
            AgregarArista("Ana", "Sofia");
            AgregarArista("Carlos", "Javier");
            AgregarArista("Maria", "Diego");
            AgregarArista("Pedro", "Elena");
            AgregarArista("Laura", "Miguel");
            AgregarArista("Ana", "Javier");
        }
    }

    public class CalculadorCentralidad
    {
        private Grafo grafo;
        private Stopwatch temporizador;

        public CalculadorCentralidad(Grafo grafo)
        {
            this.grafo = grafo;
            this.temporizador = new Stopwatch();
        }

        public Dictionary<string, double> CalcularCentralidadGrado()
        {
            temporizador.Restart();
            var centralidad = new Dictionary<string, double>();
            int totalNodos = grafo.Nodos.Count;

            foreach (string nodo in grafo.Nodos)
            {
                int grado = grafo.ListaAdyacencia.ContainsKey(nodo) ? 
                           grafo.ListaAdyacencia[nodo].Count : 0;
                double centralidadNodo = (double)grado / (totalNodos - 1);
                centralidad[nodo] = centralidadNodo;
            }

            temporizador.Stop();
            return centralidad;
        }

        public Dictionary<string, double> CalcularCentralidadCercania()
        {
            temporizador.Restart();
            var centralidad = new Dictionary<string, double>();

            foreach (string nodo in grafo.Nodos)
            {
                var distancias = CalcularDistanciasBFS(nodo);
                double sumaDistancias = distancias.Values.Where(d => d > 0).Sum();
                int nodosAlcanzables = distancias.Values.Count(d => d > 0) - 1;

                if (nodosAlcanzables > 0 && sumaDistancias > 0)
                    centralidad[nodo] = (double)nodosAlcanzables / sumaDistancias;
                else
                    centralidad[nodo] = 0.0;
            }

            temporizador.Stop();
            return centralidad;
        }

        public Dictionary<string, double> CalcularCentralidadIntermediacion()
        {
            temporizador.Restart();
            var intermediacion = grafo.Nodos.ToDictionary(nodo => nodo, nodo => 0.0);

            foreach (string fuente in grafo.Nodos)
            {
                foreach (string objetivo in grafo.Nodos)
                {
                    if (fuente == objetivo) continue;

                    var todosCaminos = EncontrarTodosCaminos(fuente, objetivo);
                    foreach (var camino in todosCaminos)
                    {
                        foreach (string nodoIntermedio in camino.Skip(1).Take(camino.Count - 2))
                        {
                            intermediacion[nodoIntermedio] += 1.0 / todosCaminos.Count;
                        }
                    }
                }
            }

            // Normalizar
            double factorNormalizacion = (grafo.Nodos.Count - 1) * (grafo.Nodos.Count - 2);
            foreach (string nodo in grafo.Nodos)
            {
                intermediacion[nodo] /= factorNormalizacion;
            }

            temporizador.Stop();
            return intermediacion;
        }

        private Dictionary<string, int> CalcularDistanciasBFS(string nodoInicio)
        {
            var distancias = grafo.Nodos.ToDictionary(nodo => nodo, nodo => -1);
            distancias[nodoInicio] = 0;

            var cola = new Queue<string>();
            cola.Enqueue(nodoInicio);

            while (cola.Count > 0)
            {
                string actual = cola.Dequeue();
                foreach (string vecino in grafo.ListaAdyacencia[actual])
                {
                    if (distancias[vecino] == -1)
                    {
                        distancias[vecino] = distancias[actual] + 1;
                        cola.Enqueue(vecino);
                    }
                }
            }

            return distancias;
        }

        private List<List<string>> EncontrarTodosCaminos(string inicio, string fin)
        {
            var caminos = new List<List<string>>();
            var caminoActual = new List<string>();
            var visitados = new HashSet<string>();

            BuscarCaminosDFS(inicio, fin, caminoActual, visitados, caminos);
            return caminos;
        }

        private void BuscarCaminosDFS(string actual, string objetivo, List<string> caminoActual, 
                                    HashSet<string> visitados, List<List<string>> caminos)
        {
            caminoActual.Add(actual);
            visitados.Add(actual);

            if (actual == objetivo)
            {
                caminos.Add(new List<string>(caminoActual));
            }
            else
            {
                foreach (string vecino in grafo.ListaAdyacencia[actual])
                {
                    if (!visitados.Contains(vecino))
                    {
                        BuscarCaminosDFS(vecino, objetivo, caminoActual, visitados, caminos);
                    }
                }
            }

            caminoActual.RemoveAt(caminoActual.Count - 1);
            visitados.Remove(actual);
        }

        public long ObtenerTiempoEjecucion()
        {
            return temporizador.ElapsedMilliseconds;
        }
    }

    public class Reporteador
    {
        public void MostrarEstructuraGrafo(Grafo grafo)
        {
            Console.WriteLine("🎯 ESTRUCTURA DEL GRAFO");
            Console.WriteLine("========================");
            Console.WriteLine($"Total nodos: {grafo.Nodos.Count}");
            Console.WriteLine($"Total aristas: {grafo.ListaAdyacencia.Sum(x => x.Value.Count) / 2}");
            
            Console.WriteLine("\nLista de adyacencia:");
            foreach (string nodo in grafo.Nodos.OrderBy(n => n))
            {
                var vecinos = grafo.ListaAdyacencia[nodo];
                Console.WriteLine($"  {nodo}: [{string.Join(", ", vecinos.OrderBy(v => v))}]");
            }
        }

        public void MostrarMetricasCentralidad(Dictionary<string, double> centralidadGrado,
                                             Dictionary<string, double> centralidadCercania,
                                             Dictionary<string, double> centralidadIntermediacion)
        {
            Console.WriteLine("\n📊 MÉTRICAS DE CENTRALIDAD");
            Console.WriteLine("==========================");

            Console.WriteLine("\nCentralidad de Grado:");
            Console.WriteLine("---------------------");
            foreach (var item in centralidadGrado.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"  {item.Key}: {item.Value:F3}");
            }

            Console.WriteLine("\nCentralidad de Cercanía:");
            Console.WriteLine("-----------------------");
            foreach (var item in centralidadCercania.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"  {item.Key}: {item.Value:F3}");
            }

            Console.WriteLine("\nCentralidad de Intermediación:");
            Console.WriteLine("-----------------------------");
            foreach (var item in centralidadIntermediacion.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"  {item.Key}: {item.Value:F3}");
            }
        }

        public void MostrarAnalisisComparativo(Dictionary<string, double>[] metricas)
        {
            Console.WriteLine("\n🔍 ANÁLISIS COMPARATIVO");
            Console.WriteLine("======================");
            
            Console.WriteLine($"{"NODO",-10} {"GRADO",-8} {"CERCANÍA",-10} {"INTERMEDIACIÓN",-15}");
            Console.WriteLine(new string('-', 50));

            var nodos = metricas[0].Keys.OrderByDescending(n => metricas[0][n]);
            foreach (string nodo in nodos)
            {
                Console.WriteLine($"{nodo,-10} {metricas[0][nodo]:F3} {metricas[1][nodo]:F3} {metricas[2][nodo]:F3}");
            }
        }

        public void MostrarTopInfluenciadores(Dictionary<string, double>[] metricas)
        {
            Console.WriteLine("\n🏆 TOP INFLUENCIADORES");
            Console.WriteLine("====================");

            var ranking = new Dictionary<string, double>();
            foreach (string nodo in metricas[0].Keys)
            {
                double puntajeTotal = metricas[0][nodo] + metricas[1][nodo] + metricas[2][nodo];
                ranking[nodo] = puntajeTotal;
            }

            int posicion = 1;
            foreach (var item in ranking.OrderByDescending(x => x.Value).Take(3))
            {
                Console.WriteLine($"{posicion}. {item.Key} - Puntaje: {item.Value:F3}");
                posicion++;
            }
        }
    }

    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🔬 CALCULADOR DE MÉTRICAS DE CENTRALIDAD");
            Console.WriteLine("========================================\n");

            // Crear y configurar grafo
            Grafo grafo = new Grafo();
            grafo.CargarEjemploRedSocial();

            // Inicializar componentes
            CalculadorCentralidad calculador = new CalculadorCentralidad(grafo);
            Reporteador reporteador = new Reporteador();

            // Mostrar estructura del grafo
            reporteador.MostrarEstructuraGrafo(grafo);

            // Calcular métricas
            Console.WriteLine("\n⏳ Calculando métricas...");

            var centralidadGrado = calculador.CalcularCentralidadGrado();
            Console.WriteLine($"✓ Centralidad de grado: {calculador.ObtenerTiempoEjecucion()} ms");

            var centralidadCercania = calculador.CalcularCentralidadCercania();
            Console.WriteLine($"✓ Centralidad de cercanía: {calculador.ObtenerTiempoEjecucion()} ms");

            var centralidadIntermediacion = calculador.CalcularCentralidadIntermediacion();
            Console.WriteLine($"✓ Centralidad de intermediación: {calculador.ObtenerTiempoEjecucion()} ms");

            // Generar reportes
            reporteador.MostrarMetricasCentralidad(centralidadGrado, centralidadCercania, centralidadIntermediacion);
            
            Dictionary<string, double>[] todasMetricas = { centralidadGrado, centralidadCercania, centralidadIntermediacion };
            reporteador.MostrarAnalisisComparativo(todasMetricas);
            reporteador.MostrarTopInfluenciadores(todasMetricas);

            // Análisis de rendimiento
            RealizarPruebasRendimiento(calculador, grafo);

            Console.WriteLine("\n🎯 Análisis completado. Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static void RealizarPruebasRendimiento(CalculadorCentralidad calculador, Grafo grafo)
        {
            Console.WriteLine("\n📈 PRUEBAS DE RENDIMIENTO");
            Console.WriteLine("========================");

            var temporizadorGlobal = new Stopwatch();
            
            // Prueba centralidad de grado
            temporizadorGlobal.Start();
            for (int i = 0; i < 1000; i++)
            {
                calculador.CalcularCentralidadGrado();
            }
            temporizadorGlobal.Stop();
            Console.WriteLine($"• 1000 cálculos de grado: {temporizadorGlobal.ElapsedMilliseconds} ms");

            // Prueba centralidad de cercanía
            temporizadorGlobal.Restart();
            for (int i = 0; i < 100; i++)
            {
                calculador.CalcularCentralidadCercania();
            }
            temporizadorGlobal.Stop();
            Console.WriteLine($"• 100 cálculos de cercanía: {temporizadorGlobal.ElapsedMilliseconds} ms");

            // Prueba centralidad de intermediación
            temporizadorGlobal.Restart();
            for (int i = 0; i < 10; i++)
            {
                calculador.CalcularCentralidadIntermediacion();
            }
            temporizadorGlobal.Stop();
            Console.WriteLine($"• 10 cálculos de intermediación: {temporizadorGlobal.ElapsedMilliseconds} ms");

            Console.WriteLine($"\n• Total nodos en grafo: {grafo.Nodos.Count}");
            Console.WriteLine($"• Total aristas en grafo: {grafo.ListaAdyacencia.Sum(x => x.Value.Count) / 2}");
        }
    }
}
