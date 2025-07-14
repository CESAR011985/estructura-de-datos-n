using System;
using System.Collections.Generic;

class Torre
{
    public Stack<int> Pila { get; private set; }
    public string Nombre { get; private set; }
    public string Color { get; private set; }

    public Torre(string nombre, string color)
    {
        Nombre = nombre;
        Color = color;
        Pila = new Stack<int>();
    }

    public void Push(int disco)
    {
        if (Pila.Count > 0 && Pila.Peek() < disco)
            throw new InvalidOperationException($"No se puede colocar el disco {disco} sobre uno más pequeño en la torre {Nombre} ({Color}).");
        Pila.Push(disco);
    }

    public int Pop()
    {
        if (Pila.Count == 0)
            throw new InvalidOperationException($"La torre {Nombre} está vacía.");
        return Pila.Pop();
    }

    public void Mostrar()
    {
        Console.Write($"{Nombre} ({Color}): ");
        foreach (var d in Pila)
        {
            Console.Write($"{d} ");
        }
        Console.WriteLine();
    }
}

class Programa
{
    static void MoverDisco(Torre origen, Torre destino)
    {
        int disco = origen.Pop();
        destino.Push(disco);
        Console.WriteLine($"Mover disco {disco} de {origen.Nombre} ({origen.Color}) a {destino.Nombre} ({destino.Color})");
    }

    static void Hanoi(int n, Torre origen, Torre auxiliar, Torre destino)
    {
        if (n == 1)
        {
            MoverDisco(origen, destino);
        }
        else
        {
            Hanoi(n - 1, origen, destino, auxiliar);
            MoverDisco(origen, destino);
            Hanoi(n - 1, auxiliar, origen, destino);
        }
    }

    static void Main()
    {
        int n = 3;

        // Datos personales
        string estudiante = "JULIO CESAR TRUJILLO";
        string codigo = "246";

        // Mostrar encabezado personalizado
        Console.WriteLine($"Alumno: {estudiante}   Código: {codigo}");
        Console.WriteLine("Simulación del problema de las Torres de Hanoi\n");

        // Colores personalizados
        Torre A = new Torre("A", "Verde");
        Torre B = new Torre("B", "Amarillo");
        Torre C = new Torre("C", "Azul");

        for (int i = n; i >= 1; i--)
            A.Push(i);

        Console.WriteLine("Estado inicial:");
        A.Mostrar();
        B.Mostrar();
        C.Mostrar();
        Console.WriteLine();

        Console.WriteLine("Pasos para resolver:");
        Hanoi(n, A, B, C);

        Console.WriteLine("\nEstado final:");
        A.Mostrar();
        B.Mostrar();
        C.Mostrar();
    }
}
