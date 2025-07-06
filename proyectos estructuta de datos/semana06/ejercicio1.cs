// See https://aka.ms/new-console-template for more information
using System;

namespace RegistroVehiculos
{
    class Nodo
    {
        public string Placa;
        public string Marca;
        public string Modelo;
        public int Anio;
        public double Precio;
        public Nodo Siguiente;

        public Nodo(string placa, string marca, string modelo, int anio, double precio)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Anio = anio;
            Precio = precio;
            Siguiente = null;
        }
    }

    class ListaVehiculos
    {
        private Nodo cabeza;

        public void AgregarVehiculo(string placa, string marca, string modelo, int anio, double precio)
        {
            Nodo nuevo = new Nodo(placa, marca, modelo, anio, precio);
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
        }

        public void BuscarPorPlaca(string placa)
        {
            Nodo actual = cabeza;
            while (actual != null)
            {
                if (actual.Placa == placa)
                {
                    Mostrar(actual);
                    return;
                }
                actual = actual.Siguiente;
            }
            Console.WriteLine("🚫 Vehículo no encontrado.\n");
        }

        public void VerPorAnio(int anio)
        {
            Nodo actual = cabeza;
            bool encontrado = false;
            while (actual != null)
            {
                if (actual.Anio == anio)
                {
                    Mostrar(actual);
                    encontrado = true;
                }
                actual = actual.Siguiente;
            }

            if (!encontrado)
                Console.WriteLine($"🚫 No se encontraron vehículos del año {anio}.\n");
        }

        public void VerTodos()
        {
            Nodo actual = cabeza;
            if (actual == null)
            {
                Console.WriteLine("🚫 No hay vehículos registrados.\n");
                return;
            }

            Console.WriteLine("📋 Lista de vehículos registrados:\n");
            while (actual != null)
            {
                Mostrar(actual);
                actual = actual.Siguiente;
            }
        }

        public void EliminarPorPlaca(string placa)
        {
            Nodo actual = cabeza;
            Nodo anterior = null;

            while (actual != null)
            {
                if (actual.Placa == placa)
                {
                    if (anterior == null)
                        cabeza = actual.Siguiente;
                    else
                        anterior.Siguiente = actual.Siguiente;

                    Console.WriteLine("🗑️ Vehículo eliminado con éxito.\n");
                    return;
                }
                anterior = actual;
                actual = actual.Siguiente;
            }

            Console.WriteLine("🚫 Vehículo no encontrado.\n");
        }

        private void Mostrar(Nodo v)
        {
            Console.WriteLine($"🚗 Placa: {v.Placa} | Marca: {v.Marca} | Modelo: {v.Modelo} | Año: {v.Anio} | Precio: ${v.Precio}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaVehiculos lista = new ListaVehiculos();

            // ✅ Datos precargados
            lista.AgregarVehiculo("POU-1122", "Chevrolet", "Cedan", 2010, 9500);
            lista.AgregarVehiculo("PZE-321", "Mazda", "Deportivo", 2015, 12000);
            lista.AgregarVehiculo("PTZ-543", "FAW", "Sub", 1998, 5000);
            lista.AgregarVehiculo("PZH7653", "Fiat", "Camioneta", 2017, 14000);
            lista.AgregarVehiculo("PZJ-9876", "Lada", "Van", 2022, 18000);
            lista.AgregarVehiculo("PXBG543", "Skoda", "Hacsh Bac", 2000, 7000);

            int opcion;
            do
            {
                Console.WriteLine("\n===== MENÚ - Estacionamiento Ingeniería de Sistemas =====");
                Console.WriteLine("1. Agregar vehículo");
                Console.WriteLine("2. Buscar por placa");
                Console.WriteLine("3. Ver por año");
                Console.WriteLine("4. Ver todos los vehículos");
                Console.WriteLine("5. Eliminar vehículo por placa");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Placa: ");
                        string placa = Console.ReadLine();
                        Console.Write("Marca: ");
                        string marca = Console.ReadLine();
                        Console.Write("Modelo: ");
                        string modelo = Console.ReadLine();
                        Console.Write("Año: ");
                        int anio = int.Parse(Console.ReadLine());
                        Console.Write("Precio: ");
                        double precio = double.Parse(Console.ReadLine());
                        lista.AgregarVehiculo(placa, marca, modelo, anio, precio);
                        Console.WriteLine("✅ Vehículo agregado.\n");
                        break;

                    case 2:
                        Console.Write("Ingrese placa a buscar: ");
                        lista.BuscarPorPlaca(Console.ReadLine());
                        break;

                    case 3:
                        Console.Write("Ingrese año a buscar: ");
                        lista.VerPorAnio(int.Parse(Console.ReadLine()));
                        break;

                    case 4:
                        lista.VerTodos();
                        break;

                    case 5:
                        Console.Write("Ingrese placa a eliminar: ");
                        lista.EliminarPorPlaca(Console.ReadLine());
                        break;

                    case 0:
                        Console.WriteLine("👋 Saliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("❗ Opción inválida.");
                        break;
                }

            } while (opcion != 0);
        }
    }
}

