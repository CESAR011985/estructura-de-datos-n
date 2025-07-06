// See https://aka.ms/new-console-template for more information
using System;
using CompararListas;

ListaEnlazada lista1 = new ListaEnlazada();
ListaEnlazada lista2 = new ListaEnlazada();

Console.Write("Ingrese la cantidad de elementos para la primera lista: ");
int n1 = int.Parse(Console.ReadLine());

Console.WriteLine("Ingrese los datos de la primera lista (se insertan por el INICIO):");
for (int i = 0; i < n1; i++)
{
    Console.Write($"Dato {i + 1}: ");
    int valor = int.Parse(Console.ReadLine());
    lista1.InsertarAlInicio(valor);
}

Console.Write("Ingrese la cantidad de elementos para la segunda lista: ");
int n2 = int.Parse(Console.ReadLine());

Console.WriteLine("Ingrese los datos de la segunda lista (se insertan por el INICIO):");
for (int i = 0; i < n2; i++)
{
    Console.Write($"Dato {i + 1}: ");
    int valor = int.Parse(Console.ReadLine());
    lista2.InsertarAlInicio(valor);
}

// Mostrar resultados (opcional)
Console.WriteLine("\nLista 1:");
lista1.Mostrar();
Console.WriteLine("Lista 2:");
lista2.Mostrar();

// Verificar condiciones
bool mismaLongitud = lista1.Longitud() == lista2.Longitud();
bool mismoContenido = ListaEnlazada.SonIguales(lista1, lista2);

Console.WriteLine("\nResultado de la comparación:");
if (mismaLongitud && mismoContenido)
{
    Console.WriteLine("a. Las listas son IGUALES en tamaño y contenido.");
}
else if (mismaLongitud)
{
    Console.WriteLine("b. Las listas son IGUALES en tamaño pero DIFERENTES en contenido.");
}
else
{
    Console.WriteLine("c. Las listas NO tienen el mismo tamaño ni contenido.");
}

Console.ReadLine(); // Para pausar consola

namespace CompararListas
{
    // Clase Nodo
    public class Nodo
    {
        public int Valor;
        public Nodo Siguiente;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        public Nodo(int valor)
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        {
            Valor = valor;
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            Siguiente = null;
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        }
    }

    // Clase Lista Enlazada
    public class ListaEnlazada
    {
        public Nodo Cabeza;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        public ListaEnlazada()
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        {
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            Cabeza = null;
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
        }

        // Insertar por el inicio
        public void InsertarAlInicio(int valor)
        {
            Nodo nuevo = new Nodo(valor);
            nuevo.Siguiente = Cabeza;
            Cabeza = nuevo;
        }

        // Obtener la longitud de la lista
        public int Longitud()
        {
            int contador = 0;
            Nodo actual = Cabeza;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }

        // Comparar contenido de dos listas
        public static bool SonIguales(ListaEnlazada l1, ListaEnlazada l2)
        {
            Nodo a = l1.Cabeza;
            Nodo b = l2.Cabeza;
            while (a != null && b != null)
            {
                if (a.Valor != b.Valor)
                    return false;
                a = a.Siguiente;
                b = b.Siguiente;
            }
            return a == null && b == null;
        }

        // Mostrar contenido (para depuración)
        public void Mostrar()
        {
            Nodo actual = Cabeza;
            while (actual != null)
            {
                Console.Write(actual.Valor + " -> ");
                actual = actual.Siguiente;
            }
            Console.WriteLine("null");
        }
    }
}
