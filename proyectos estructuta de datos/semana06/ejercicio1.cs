// See https://aka.ms/new-console-template for more information
using System;

public class Nodo
{
    public int Valor { get; set; }
    public Nodo Siguiente { get; set; }
    
    public Nodo(int valor) => Valor = valor;
}

public class ListaEnlazada
{
    private Nodo cabeza;
    private readonly Random random = new();
    
    public void GenerarLista()
    {
        for (int i = 0; i < 50; i++)
            AgregarNodo(random.Next(1, 1000));
    }
    
    private void AgregarNodo(int valor)
    {
        var nuevo = new Nodo(valor);
        
        if (cabeza == null)
        {
            cabeza = nuevo;
            return;
        }
        
        var actual = cabeza;
        while (actual.Siguiente != null)
            actual = actual.Siguiente;
            
        actual.Siguiente = nuevo;
    }
    
    public void Filtrar(int min, int max)
    {
        while (cabeza != null && (cabeza.Valor < min || cabeza.Valor > max))
            cabeza = cabeza.Siguiente;
            
        if (cabeza == null) return;
        
        var actual = cabeza;
        while (actual.Siguiente != null)
        {
            if (actual.Siguiente.Valor < min || actual.Siguiente.Valor > max)
                actual.Siguiente = actual.Siguiente.Siguiente;
            else
                actual = actual.Siguiente;
        }
    }
    
    public void Mostrar()
    {
        var actual = cabeza;
        int contador = 0;
        
        while (actual != null)
        {
            Console.Write($"{actual.Valor} ");
            if (++contador % 10 == 0) Console.WriteLine();
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        var lista = new ListaEnlazada();
        lista.GenerarLista();
        
        Console.WriteLine("Lista original:");
        lista.Mostrar();
        
        Console.Write("Ingrese mínimo: ");
        int min = int.Parse(Console.ReadLine());
        Console.Write("Ingrese máximo: ");
        int max = int.Parse(Console.ReadLine());
        
        lista.Filtrar(min, max);
        
        Console.WriteLine("Lista filtrada:");
        lista.Mostrar();
    }
}