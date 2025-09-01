using System;
using System.Collections.Generic;
using System.Text;

class Traductor
{
    static void Main()
    {
        // Diccionario mejorado con palabras alternativas
        Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"time", "momento"},
            {"person", "individuo"},
            {"year", "exercicio"},
            {"way", "sendero"},
            {"day", "jornada"},
            {"thing", "objeto"},
            {"man", "caballero"},
            {"world", "planeta"},
            {"life", "existencia"},
            {"hand", "extremidad"},
            {"part", "porción"},
            {"child", "infante"},
            {"eye", "mirada"},
            {"woman", "dama"},
            {"place", "ubicación"},
            {"work", "labor"},
            {"week", "semana"},
            {"case", "situación"},
            {"point", "aspecto"},
            {"government", "administración"},
            {"company", "corporación"},
            {"beautiful", "hermoso"},
            {"good", "bueno"},
            {"big", "grande"},
            {"small", "pequeño"},
            {"house", "hogar"},
            {"dog", "perro"},
            {"cat", "felino"},
            {"book", "libro"},
            {"water", "agua"}
        };

        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("3. Ver todas las palabras del diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("\nIngrese la frase: ");
                    string frase = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(frase))
                    {
                        string traduccion = TraducirFrase(frase, diccionario);
                        Console.WriteLine("Traducción: " + traduccion);
                    }
                    else
                    {
                        Console.WriteLine("No se ingresó ninguna frase.");
                    }
                    break;

                case "2":
                    AgregarPalabra(diccionario);
                    break;

                case "3":
                    MostrarDiccionario(diccionario);
                    break;

                case "0":
                    salir = true;
                    Console.WriteLine("Saliendo del traductor...");
                    break;

                default:
                    Console.WriteLine("Opción no válida, intente de nuevo.");
                    break;
            }
        }
    }

    // Método mejorado para traducir frases
    static string TraducirFrase(string frase, Dictionary<string, string> diccionario)
    {
        StringBuilder resultado = new StringBuilder();
        string[] palabras = frase.Split(' ');
        
        foreach (string palabra in palabras)
        {
            string palabraTraducida = TraducirPalabra(palabra, diccionario);
            resultado.Append(palabraTraducida + " ");
        }
        
        return resultado.ToString().Trim();
    }

    // Método para traducir una palabra individual
    static string TraducirPalabra(string palabra, Dictionary<string, string> diccionario)
    {
        // Limpiar la palabra de signos de puntuación
        string palabraLimpia = LimpiarPalabra(palabra);
        
        if (string.IsNullOrWhiteSpace(palabraLimpia))
            return palabra;

        // Verificar si la palabra existe en el diccionario
        if (diccionario.TryGetValue(palabraLimpia, out string traduccion))
        {
            return AplicarFormatoOriginal(palabra, traduccion);
        }
        
        // Si no se encuentra, devolver la palabra original
        return palabra;
    }

    // Limpiar signos de puntuación
    static string LimpiarPalabra(string palabra)
    {
        char[] signosPuntuacion = { '.', ',', ';', ':', '!', '?', '"', '\'', '(', ')', '[', ']', '{', '}' };
        return palabra.Trim(signosPuntuacion);
    }

    // Preservar el formato original (mayúsculas, puntuación)
    static string AplicarFormatoOriginal(string palabraOriginal, string traduccion)
    {
        if (palabraOriginal == traduccion)
            return palabraOriginal;

        // Preservar mayúscula inicial
        if (char.IsUpper(palabraOriginal[0]))
        {
            traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);
        }

        // Preservar signos de puntuación
        string palabraLimpia = LimpiarPalabra(palabraOriginal);
        int indiceFin = palabraOriginal.IndexOf(palabraLimpia) + palabraLimpia.Length;
        
        if (indiceFin < palabraOriginal.Length)
        {
            string signos = palabraOriginal.Substring(indiceFin);
            traduccion += signos;
        }

        return traduccion;
    }

    // Método para agregar palabras al diccionario
    static void AgregarPalabra(Dictionary<string, string> diccionario)
    {
        Console.Write("\nIngrese la palabra en inglés: ");
        string ingles = Console.ReadLine()?.Trim();
        Console.Write("Ingrese la traducción en español: ");
        string espanol = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(ingles) || string.IsNullOrWhiteSpace(espanol))
        {
            Console.WriteLine("Error: Ambas palabras deben tener contenido.");
            return;
        }

        string inglesLimpio = ingles.ToLower();
        
        if (!diccionario.ContainsKey(inglesLimpio))
        {
            diccionario.Add(inglesLimpio, espanol);
            Console.WriteLine($"Palabra '{ingles}' → '{espanol}' agregada correctamente.");
        }
        else
        {
            Console.WriteLine("La palabra ya existe en el diccionario.");
            Console.WriteLine($"Traducción actual: {diccionario[inglesLimpio]}");
        }
    }

    // Método para mostrar todo el diccionario
    static void MostrarDiccionario(Dictionary<string, string> diccionario)
    {
        Console.WriteLine("\n=== CONTENIDO DEL DICCIONARIO ===");
        foreach (var entrada in diccionario)
        {
            Console.WriteLine($"{entrada.Key} → {entrada.Value}");
        }
        Console.WriteLine($"Total: {diccionario.Count} palabras");
    }
}