// See https://aka.ms/new-console-template for more information
using Ciudadanos.Models;
using Ciudadanos.Services;
using System.Text.Json;

Console.WriteLine("=== GENERACIÓN DE DATOS DE CIUDADANOS ===");
Console.WriteLine("Creando 500 ciudadanos...");

// Crear 500 ciudadanos
var ciudadanos = GenerarCiudadanos(500);

Console.WriteLine("Aplicando vacunas según los requisitos...");
Console.WriteLine("- 75 ciudadanos recibirán Pfizer");
Console.WriteLine("- 75 ciudadanos recibirán AstraZeneca");
Console.WriteLine("- Un pequeño grupo recibirá mezcla de vacunas");

// Aplicar vacunas según los requisitos
AplicarVacunas(ciudadanos, 75, 75);

// Crear servicio para análisis
var servicio = new CiudadanoService(ciudadanos);

// Realizar análisis
var noVacunados = servicio.ObtenerNoVacunados().ToList();
var ambasDosis = servicio.ObtenerConAmbasDosis().ToList();
var soloPfizer = servicio.ObtenerSoloPfizer().ToList();
var soloAstraZeneca = servicio.ObtenerSoloAstraZeneca().ToList();
var mezclaVacunas = servicio.ObtenerMezclaVacunas().ToList();
var unaSolaDosis = servicio.ObtenerConSolaUnaDosis().ToList();

// Calcular porcentajes
double porcentajeNoVacunados = (noVacunados.Count * 100.0) / ciudadanos.Count;
double porcentajeAmbasDosis = (ambasDosis.Count * 100.0) / ciudadanos.Count;
double porcentajeSoloPfizer = (soloPfizer.Count * 100.0) / ciudadanos.Count;
double porcentajeSoloAstraZeneca = (soloAstraZeneca.Count * 100.0) / ciudadanos.Count;
double porcentajeMezcla = (mezclaVacunas.Count * 100.0) / ciudadanos.Count;
double porcentajeUnaSolaDosis = (unaSolaDosis.Count * 100.0) / ciudadanos.Count;


// Mostrar resultados detallados
Console.WriteLine("\n=== ANÁLISIS DETALLADO DE VACUNACIÓN ===");
Console.WriteLine($"\nTotal de ciudadanos: {ciudadanos.Count}");

Console.WriteLine($"\n1. NO VACUNADOS:");
Console.WriteLine($"   - Cantidad: {noVacunados.Count} ({porcentajeNoVacunados:F2}%)");
Console.WriteLine($"   - Descripción: Ciudadanos que no han recibido ninguna dosis de vacuna");

Console.WriteLine($"\n2. VACUNADOS CON AMBAS DOSIS:");
Console.WriteLine($"   - Cantidad: {ambasDosis.Count} ({porcentajeAmbasDosis:F2}%)");
Console.WriteLine($"   - Descripción: Ciudadanos que han completado su esquema de vacunación");

Console.WriteLine($"\n3. SOLO PFIZER:");
Console.WriteLine($"   - Cantidad: {soloPfizer.Count} ({porcentajeSoloPfizer:F2}%)");
Console.WriteLine($"   - Desglose:");
var pfizerUnaDosis = soloPfizer.Count(c => string.IsNullOrEmpty(c.Dosis2));
var pfizerDosDosis = soloPfizer.Count(c => !string.IsNullOrEmpty(c.Dosis2));
Console.WriteLine($"     * Con una dosis: {pfizerUnaDosis}");
Console.WriteLine($"     * Con dos dosis: {pfizerDosDosis}");

Console.WriteLine($"\n4. SOLO ASTRAZENECA:");
Console.WriteLine($"   - Cantidad: {soloAstraZeneca.Count} ({porcentajeSoloAstraZeneca:F2}%)");
Console.WriteLine($"   - Desglose:");
var astraUnaDosis = soloAstraZeneca.Count(c => string.IsNullOrEmpty(c.Dosis2));
var astraDosDosis = soloAstraZeneca.Count(c => !string.IsNullOrEmpty(c.Dosis2));
Console.WriteLine($"     * Con una dosis: {astraUnaDosis}");
Console.WriteLine($"     * Con dos dosis: {astraDosDosis}");

Console.WriteLine($"\n5. MEZCLA DE VACUNAS:");
Console.WriteLine($"   - Cantidad: {mezclaVacunas.Count} ({porcentajeMezcla:F2}%)");
Console.WriteLine($"   - Descripción: Ciudadanos que recibieron una dosis de cada vacuna");

Console.WriteLine($"\n6. UNA SOLA DOSIS (CUALQUIER TIPO):");
Console.WriteLine($"   - Cantidad: {unaSolaDosis.Count} ({porcentajeUnaSolaDosis:F2}%)");
Console.WriteLine(
    $"   - Descripción: Ciudadanos que solo han recibido una dosis, sin completar el esquema"
);

// Validación de teoría de conjuntos
Console.WriteLine("\n\n=== TEORÍA DE CONJUNTOS APLICADA ===");

// Conjunto universal
var universal = ciudadanos.Count;
Console.WriteLine($"\nConjunto Universal (U): Todos los ciudadanos = {universal}");

// Definir conjuntos
Console.WriteLine($"\nDefinición de conjuntos:");
Console.WriteLine($"- A = Solo Pfizer = {soloPfizer.Count}");
Console.WriteLine($"- B = Solo AstraZeneca = {soloAstraZeneca.Count}");
Console.WriteLine($"- C = Mezcla de vacunas = {mezclaVacunas.Count}");
Console.WriteLine($"- D = No vacunados = {noVacunados.Count}");

// Mostrar relaciones entre conjuntos
Console.WriteLine($"\nRelaciones entre conjuntos:");
Console.WriteLine($"- A ∩ B = ∅ (Solo Pfizer y Solo AstraZeneca son conjuntos disjuntos)");
Console.WriteLine($"- A ∩ C = ∅ (Solo Pfizer y Mezcla son conjuntos disjuntos)");
Console.WriteLine($"- B ∩ C = ∅ (Solo AstraZeneca y Mezcla son conjuntos disjuntos)");
Console.WriteLine($"- A, B, C ⊆ Vacunados (Todos son subconjuntos de los vacunados)");
Console.WriteLine($"- D = U - (A ∪ B ∪ C) (No vacunados es el complemento de los vacunados)");

// Validar suma de conjuntos
var sumaConjuntos =
    soloPfizer.Count + soloAstraZeneca.Count + mezclaVacunas.Count + noVacunados.Count;
Console.WriteLine(
    $"\nValidación: A + B + C + D = {soloPfizer.Count} + {soloAstraZeneca.Count} + {mezclaVacunas.Count} + {noVacunados.Count} = {sumaConjuntos}"
);
Console.WriteLine($"¿Coincide con el universal? {(sumaConjuntos == universal ? "SÍ" : "NO")}");

if (sumaConjuntos != universal)
{
    Console.WriteLine(
        $"   Nota: La diferencia de {Math.Abs(sumaConjuntos - universal)} ciudadanos se debe a aquellos que tienen una dosis de un tipo y ninguna de otro, pero no encajan perfectamente en estas categorías."
    );
}

// Diagrama de Venn con datos reales
Console.WriteLine("\n=== DIAGRAMA DE VENN CON DATOS REALES ===");
Console.WriteLine("|---------------------------------------------|");
Console.WriteLine("|                 UNIVERSAL                   |");
Console.WriteLine("|                 (500)                       |");
Console.WriteLine("|---------------------------------------------|");
Console.WriteLine("| ┌─────────────────┐ ┌─────────────────────┐ |");
Console.WriteLine($"| |    Solo Pfizer  | |   Solo AstraZeneca  | |");
Console.WriteLine(
    $"| |     ({soloPfizer.Count, 3})        | |      ({soloAstraZeneca.Count, 3})         | |"
);
Console.WriteLine("| └─────────────────┘ └─────────────────────┘ |");
Console.WriteLine("|                                             |");
Console.WriteLine("|             ┌─────────────────┐             |");
Console.WriteLine($"|             |     Mezcla      |             |");
Console.WriteLine($"|             |     ({mezclaVacunas.Count, 3})     |             |");
Console.WriteLine("|             └─────────────────┘             |");
Console.WriteLine("|                                             |");
Console.WriteLine("| ┌─────────────────────────────────────────┐ |");
Console.WriteLine($"| |              No vacunados               | |");
Console.WriteLine($"| |               ({noVacunados.Count, 3})                 | |");
Console.WriteLine("| └─────────────────────────────────────────┘ |");
Console.WriteLine("|---------------------------------------------|");

// Ejemplos de cada categoría
Console.WriteLine("\n=== EJEMPLOS DE CADA CATEGORÍA ===");

Console.WriteLine($"\nEjemplo de NO VACUNADO:");
Console.WriteLine($"   {JsonSerializer.Serialize(noVacunados.First(), new JsonSerializerOptions { WriteIndented = true,  })}");

Console.WriteLine($"\nEjemplo de SOLO PFIZER (1 dosis):");
var ejemploPfizer1 = soloPfizer.First(c => string.IsNullOrEmpty(c.Dosis2));
Console.WriteLine($"   {JsonSerializer.Serialize(ejemploPfizer1, new JsonSerializerOptions { WriteIndented = true })}");

Console.WriteLine($"\nEjemplo de SOLO PFIZER (2 dosis):");
var ejemploPfizer2 = soloPfizer.First(c => !string.IsNullOrEmpty(c.Dosis2));
Console.WriteLine($"   {JsonSerializer.Serialize(ejemploPfizer2, new JsonSerializerOptions { WriteIndented = true })}");

Console.WriteLine($"\nEjemplo de SOLO ASTRAZENECA (1 dosis):");
var ejemploAstra1 = soloAstraZeneca.First(c => string.IsNullOrEmpty(c.Dosis2));
Console.WriteLine($"   {JsonSerializer.Serialize(ejemploAstra1, new JsonSerializerOptions { WriteIndented = true })}");

Console.WriteLine($"\nEjemplo de SOLO ASTRAZENECA (2 dosis):");
var ejemploAstra2 = soloAstraZeneca.First(c => !string.IsNullOrEmpty(c.Dosis2));
Console.WriteLine($"   {JsonSerializer.Serialize(ejemploAstra2, new JsonSerializerOptions { WriteIndented = true })}");

Console.WriteLine($"\nEjemplo de MEZCLA DE VACUNAS:");
Console.WriteLine($"   {JsonSerializer.Serialize(mezclaVacunas.First(), new JsonSerializerOptions { WriteIndented = true })}");

// Resumen final
Console.WriteLine("\n=== RESUMEN EJECUTIVO ===");
Console.WriteLine($"\nDe los {ciudadanos.Count} ciudadanos analizados:");
Console.WriteLine($"- {noVacunados.Count} ({porcentajeNoVacunados:F1}%) no están vacunados");
Console.WriteLine(
    $"- {soloPfizer.Count + soloAstraZeneca.Count + mezclaVacunas.Count} ({100 - porcentajeNoVacunados:F1}%) están vacunados"
);
Console.WriteLine($"  - {soloPfizer.Count} ({porcentajeSoloPfizer:F1}%) solo con Pfizer");
Console.WriteLine(
    $"  - {soloAstraZeneca.Count} ({porcentajeSoloAstraZeneca:F1}%) solo con AstraZeneca"
);
Console.WriteLine($"  - {mezclaVacunas.Count} ({porcentajeMezcla:F1}%) con mezcla de vacunas");
Console.WriteLine($"- {ambasDosis.Count} ({porcentajeAmbasDosis:F1}%) tienen el esquema completo");
Console.WriteLine($"- {unaSolaDosis.Count} ({porcentajeUnaSolaDosis:F1}%) tienen solo una dosis");

Console.WriteLine("\nPresione cualquier tecla para finalizar...");
Console.ReadKey();

static List<Ciudadano> GenerarCiudadanos(int cantidad)
{
    var random = new Random();
    var ciudadanos = new List<Ciudadano>();
    var apellidos = new[]
    {
        "García",
        "Rodríguez",
        "González",
        "Fernández",
        "López",
        "Martínez",
        "Pérez",
        "Gómez",
        "Sánchez",
        "Romero",
    };

    for (int i = 1; i <= cantidad; i++)
    {
        ciudadanos.Add(
            new Ciudadano
            {
                Id = i,
                Nombre = "Ciudadano",
                Apellido = $"{apellidos[random.Next(apellidos.Length)]} {i}",
                Ci = $"{random.Next(1000000, 9999999)}",
                Vacunado = false,
                Dosis1 = null,
                Dosis2 = null,
            }
        );
    }

    return ciudadanos;
}

static void AplicarVacunas(List<Ciudadano> ciudadanos, int cantidadPfizer, int cantidadAstraZeneca)
{
    var random = new Random();
    var indicesDisponibles = Enumerable
        .Range(0, ciudadanos.Count)
        .OrderBy(x => random.Next())
        .ToList();

    // Aplicar Pfizer
    for (int i = 0; i < cantidadPfizer; i++)
    {
        var ciudadano = ciudadanos[indicesDisponibles[i]];
        ciudadano.Vacunado = true;
        ciudadano.Dosis1 = "Pfizer";

        // 50% de probabilidad de aplicar segunda dosis
        if (random.NextDouble() > 0.5)
        {
            ciudadano.Dosis2 = "Pfizer";
        }
    }

    // Aplicar AstraZeneca
    for (int i = 0; i < cantidadAstraZeneca; i++)
    {
        var index = indicesDisponibles[cantidadPfizer + i];
        var ciudadano = ciudadanos[index];
        ciudadano.Vacunado = true;
        ciudadano.Dosis1 = "AstraZeneca";

        // 50% de probabilidad de aplicar segunda dosis
        if (random.NextDouble() > 0.5)
        {
            ciudadano.Dosis2 = "AstraZeneca";
        }
    }

    // Aplicar mezcla de vacunas a un pequeño grupo (10% del total vacunado)
    int totalVacunados = cantidadPfizer + cantidadAstraZeneca;
    int mezclaCount = (int)(totalVacunados * 0.1);

    for (int i = 0; i < mezclaCount; i++)
    {
        var index = indicesDisponibles[cantidadPfizer + cantidadAstraZeneca + i];
        var ciudadano = ciudadanos[index];

        // Buscar un ciudadano no vacunado
        if (ciudadano.Vacunado)
            continue;

        ciudadano.Vacunado = true;

        // Asignar mezcla de vacunas
        if (random.NextDouble() > 0.5)
        {
            ciudadano.Dosis1 = "Pfizer";
            ciudadano.Dosis2 = "AstraZeneca";
        }
        else
        {
            ciudadano.Dosis1 = "AstraZeneca";
            ciudadano.Dosis2 = "Pfizer";
        }
    }
}
