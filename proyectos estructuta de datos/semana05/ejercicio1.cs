using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string> asignaturas = new List<string>()
        {
            "Matemáticas", "Física", "Química", "Historia", "Lengua"
        };

        int opcion;
        do
        {
            Console.WriteLine("\n===== MENÚ DE ASIGNATURAS =====");
            Console.WriteLine("1. Ver asignaturas");
            Console.WriteLine("2. Agregar asignatura");
            Console.WriteLine("3. Buscar asignatura");
            Console.WriteLine("4. Eliminar asignatura");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("❌ Opción inválida. Intente de nuevo.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("\n📚 Lista de asignaturas:");
                    foreach (string asignatura in asignaturas)
                    {
                        Console.WriteLine("- " + asignatura);
                    }
                    break;

                case 2:
                    Console.Write("Ingrese el nombre de la nueva asignatura: ");
                    string nueva = Console.ReadLine();
                    if (!asignaturas.Contains(nueva))
                    {
                        asignaturas.Add(nueva);
                        Console.WriteLine("✅ Asignatura agregada.");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ La asignatura ya existe.");
                    }
                    break;

                case 3:
                    Console.Write("Ingrese el nombre de la asignatura a buscar: ");
                    string buscar = Console.ReadLine();
                    if (asignaturas.Contains(buscar))
                    {
                        Console.WriteLine("🔍 La asignatura está en la lista.");
                    }
                    else
                    {
                        Console.WriteLine("🚫 La asignatura no se encuentra.");
                    }
                    break;

                case 4:
                    Console.Write("Ingrese el nombre de la asignatura a eliminar: ");
                    string eliminar = Console.ReadLine();
                    if (asignaturas.Remove(eliminar))
                    {
                        Console.WriteLine("🗑️ Asignatura eliminada.");
                    }
                    else
                    {
                        Console.WriteLine("🚫 No se encontró la asignatura.");
                    }
                    break;

                case 0:
                    Console.WriteLine("👋 Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("❌ Opción inválida.");
                    break;
            }

        } while (opcion != 0);
    }
}
