using System;
using System.Collections.Generic;

namespace BST_Revistas
{
    // Nodo del árbol
    class Nodo
    {
        public string Titulo;
        public Nodo Izq;
        public Nodo Der;

        public Nodo(string titulo) => Titulo = titulo;
    }

    // Árbol Binario de Búsqueda
    class ArbolBST
    {
        private Nodo _raiz;

        public bool Vacio => _raiz == null;

        // Insertar (ignora duplicados)
        public void Insertar(string titulo) => _raiz = InsertarRec(_raiz, titulo);

        private Nodo InsertarRec(Nodo actual, string titulo)
        {
            if (actual == null) return new Nodo(titulo);

            int comp = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comp < 0)
                actual.Izq = InsertarRec(actual.Izq, titulo);
            else if (comp > 0)
                actual.Der = InsertarRec(actual.Der, titulo);
            // Si es igual, no se inserta (evitar duplicados)
            return actual;
        }

        // Buscar
        public bool Buscar(string titulo) => BuscarRec(_raiz, titulo);

        private bool BuscarRec(Nodo actual, string titulo)
        {
            if (actual == null) return false;
            int comp = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comp == 0) return true;
            return comp < 0
                ? BuscarRec(actual.Izq, titulo)
                : BuscarRec(actual.Der, titulo);
        }

        // Recorridos (Inorden para mostrar ordenado)
        public List<string> Inorden()
        {
            var r = new List<string>();
            InordenRec(_raiz, r);
            return r;
        }
        private void InordenRec(Nodo n, List<string> r)
        {
            if (n == null) return;
            InordenRec(n.Izq, r);
            r.Add(n.Titulo);
            InordenRec(n.Der, r);
        }

        public void Limpiar() => _raiz = null;
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var arbol = new ArbolBST();

            // Pre-cargar catálogo de revistas
            string[] revistas =
            {
                "Revista Latinoamericana de Psicología",
                "Revista de Economía Institucional",
                "Revista Mexicana de Sociología",
                "Cuadernos de Desarrollo Rural",
                "Revista de Estudios Sociales",
                "Revista Iberoamericana de Educación",
                "Revista CEPAL",
                "Revista Ecuatoriana de Medicina y Ciencias Biológicas",
                "Revista Chilena de Literatura",
                "Revista Interamericana de Bibliotecología"
            };
            foreach (var r in revistas) arbol.Insertar(r);

            while (true)
            {
                Console.WriteLine("\n=== CATÁLOGO DE REVISTAS ===");
                Console.WriteLine("1) Buscar revista");
                Console.WriteLine("2) Mostrar catálogo (Inorden)");
                Console.WriteLine("3) Agregar nueva revista");
                Console.WriteLine("4) Limpiar catálogo");
                Console.WriteLine("0) Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el título a buscar: ");
                        string titulo = Console.ReadLine();
                        Console.WriteLine(arbol.Buscar(titulo)
                            ? $"✔ \"{titulo}\" ENCONTRADO en el catálogo."
                            : $"✖ \"{titulo}\" NO está en el catálogo.");
                        break;

                    case "2":
                        var lista = arbol.Inorden();
                        if (lista.Count == 0)
                            Console.WriteLine("El catálogo está vacío.");
                        else
                            Console.WriteLine("=== Catálogo de Revistas (Ordenado) ===\n" +
                                string.Join("\n", lista));
                        break;

                    case "3":
                        Console.Write("Ingrese el título de la nueva revista: ");
                        string nueva = Console.ReadLine();
                        arbol.Insertar(nueva);
                        Console.WriteLine($"✔ \"{nueva}\" se agregó al catálogo.");
                        break;

                    case "4":
                        arbol.Limpiar();
                        Console.WriteLine("Catálogo borrado.");
                        break;

                    case "0":
                        Console.WriteLine("¡Hasta luego!");
                        return;

                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }
    }
}

