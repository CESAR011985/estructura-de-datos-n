using System;                   // ← using PRIMERO
using Otro.Namespace;           // ← Todos los using al inicio

namespace TuNamespace           // ← Luego el namespace
{
    class TuClase               // ← Finalmente las clases
    {
        // Código aquí
    }
}
using System;
using AgendaTurnos.Models;
using AgendaTurnos.Services;

namespace semana04
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var turnoService = new TurnoService();

                turnoService.AgregarTurno(new Paciente("Juan Pérez", "12345678", "Dolor de cabeza"));
                turnoService.AgregarTurno(new Paciente("María Gómez", "87654321", "Control rutinario"));

                Console.WriteLine("=== LISTA DE TURNOS ===");
                turnoService.MostrarTurnos();

                Console.WriteLine("\n=== BUSCAR TURNO ===");
                var turno = turnoService.BuscarTurno("12345678");
                Console.WriteLine(turno != null ? $"Encontrado: {turno.Paciente.Nombre}" : "No encontrado");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

