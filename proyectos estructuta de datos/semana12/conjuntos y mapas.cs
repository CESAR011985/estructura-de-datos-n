using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp
{
    public class Libro
    {
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }

        public Libro(string isbn, string titulo, string autor, string categoria)
        {
            Isbn = isbn;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return $"ISBN: {Isbn} | Título: {Titulo} | Autor: {Autor} | Categoría: {Categoria}";
        }
    }

    public class Biblioteca
    {
        private Dictionary<string, Libro> catalogo = new Dictionary<string, Libro>();
        private HashSet<string> categorias = new HashSet<string>();

        // Operaciones CRUD
        public void AgregarLibro(Libro libro)
        {
            catalogo.Add(libro.Isbn, libro);
            categorias.Add(libro.Categoria);
        }

        public Libro BuscarPorIsbn(string isbn)
        {
            return catalogo.GetValueOrDefault(isbn);
        }

        public List<Libro> BuscarPorCategoria(string categoria)
        {
            return catalogo.Values
                .Where(libro => libro.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool EliminarLibro(string isbn)
        {
            if (catalogo.Remove(isbn, out Libro libroEliminado))
            {
                // Verificar si la categoría sigue en uso
                if (!catalogo.Values.Any(libro => libro.Categoria == libroEliminado.Categoria))
                {
                    categorias.Remove(libroEliminado.Categoria);
                }
                return true;
            }
            return false;
        }

        // Reportería
        public void MostrarCatalogo()
        {
            Console.WriteLine($"\n--- CATÁLOGO COMPLETO ({catalogo.Count} libros) ---");
            foreach (var libro in catalogo.Values)
            {
                Console.WriteLine(libro);
            }
        }

        public void MostrarCategorias()
        {
            Console.WriteLine($"\n--- CATEGORÍAS DISPONIBLES ({categorias.Count}) ---");
            foreach (var categoria in categorias)
            {
                Console.WriteLine($"- {categoria}");
            }
        }

        public void MostrarLibrosPorCategoria()
        {
            Console.WriteLine("\n--- LIBROS POR CATEGORÍA ---");
            foreach (var categoria in categorias)
            {
                var libros = BuscarPorCategoria(categoria);
                Console.WriteLine($"\n[{categoria}] ({libros.Count} libros):");
                foreach (var libro in libros)
                {
                    Console.WriteLine($"  {libro.Titulo} - {libro.Autor}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            // Agregar libros de ejemplo
            biblioteca.AgregarLibro(new Libro("001", "Cien años de soledad", "Gabriel García Márquez", "Realismo mágico"));
            biblioteca.AgregarLibro(new Libro("002", "1984", "George Orwell", "Ciencia ficción"));
            biblioteca.AgregarLibro(new Libro("003", "El Aleph", "Jorge Luis Borges", "Ficción"));
            biblioteca.AgregarLibro(new Libro("004", "Fahrenheit 451", "Ray Bradbury", "Ciencia ficción"));

            // Menú interactivo
            while (true)
            {
                Console.WriteLine("\n=== SISTEMA DE BIBLIOTECA ===");
                Console.WriteLine("1. Mostrar catálogo completo");
                Console.WriteLine("2. Mostrar categorías");
                Console.WriteLine("3. Mostrar libros por categoría");
                Console.WriteLine("4. Buscar libro por ISBN");
                Console.WriteLine("5. Buscar libros por categoría");
                Console.WriteLine("6. Agregar nuevo libro");
                Console.WriteLine("7. Eliminar libro");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        biblioteca.MostrarCatalogo();
                        break;
                    case "2":
                        biblioteca.MostrarCategorias();
                        break;
                    case "3":
                        biblioteca.MostrarLibrosPorCategoria();
                        break;
                    case "4":
                        Console.Write("Ingrese ISBN: ");
                        string isbn = Console.ReadLine();
                        var libro = biblioteca.BuscarPorIsbn(isbn);
                        Console.WriteLine(libro != null ? libro.ToString() : "Libro no encontrado.");
                        break;
                    case "5":
                        Console.Write("Ingrese categoría: ");
                        string categoria = Console.ReadLine();
                        var libros = biblioteca.BuscarPorCategoria(categoria);
                        Console.WriteLine($"\nLibros en la categoría '{categoria}':");
                        foreach (var l in libros)
                        {
                            Console.WriteLine(l);
                        }
                        break;
                    case "6":
                        Console.WriteLine("\nAgregar nuevo libro:");
                        Console.Write("ISBN: ");
                        string newIsbn = Console.ReadLine();
                        Console.Write("Título: ");
                        string titulo = Console.ReadLine();
                        Console.Write("Autor: ");
                        string autor = Console.ReadLine();
                        Console.Write("Categoría: ");
                        string newCategoria = Console.ReadLine();
                        biblioteca.AgregarLibro(new Libro(newIsbn, titulo, autor, newCategoria));
                        Console.WriteLine("Libro agregado exitosamente.");
                        break;
                    case "7":
                        Console.Write("Ingrese ISBN del libro a eliminar: ");
                        string isbnEliminar = Console.ReadLine();
                        bool eliminado = biblioteca.EliminarLibro(isbnEliminar);
                        Console.WriteLine(eliminado ? "Libro eliminado." : "ISBN no encontrado.");
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }
    }
}