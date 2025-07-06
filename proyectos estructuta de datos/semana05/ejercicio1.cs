using System;
using System.Collections.Generic;
using System.Linq;

namespace Estadisticas
{
    public class CalculadoraEstadistica
    {
        private List<double> numeros;

        public CalculadoraEstadistica(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada))
                throw new ArgumentException("La entrada no puede estar vacía");

            try
            {
                numeros = entrada.Split(',')
                    .Select(s => double.Parse(s.Trim()))
                    .ToList();
            }
            catch (FormatException)
            {
                throw new ArgumentException("La entrada contiene valores no numéricos");
            }

            if (!numeros.Any())
                throw new ArgumentException("Debe ingresar al menos un número");
        }

        public double CalcularMedia()
        {
            return numeros.Average();
        }

        public double CalcularDesviacionTipica()
        {
            var media = CalcularMedia();
            var sumaCuadrados = numeros.Sum(num => Math.Pow(num - media, 2));
            return Math.Sqrt(sumaCuadrados / numeros.Count);
        }

        public void MostrarResultados()
        {
            Console.WriteLine($"Media: {CalcularMedia():F2}");
            Console.WriteLine($"Desviación típica: {CalcularDesviacionTipica():F2}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduce números separados por comas:");
            string entrada = Console.ReadLine();

            try
            {
                var calculadora = new CalculadoraEstadistica(entrada);
                calculadora.MostrarResultados();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}