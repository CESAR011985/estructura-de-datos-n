using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 1. Generar 500 ciudadanos ficticios (IDs del 1 al 500)
        HashSet<int> ciudadanos = new HashSet<int>(Enumerable.Range(1, 500));

        // 2. Conjuntos de vacunados (Pfizer y AstraZeneca)
        Random rnd = new Random();
        
        // 75 vacunados con Pfizer (A)
        HashSet<int> pfizer = new HashSet<int>();
        while (pfizer.Count < 75)
        {
            pfizer.Add(rnd.Next(1, 501));
        }

        // 75 vacunados con AstraZeneca (B), asegurando que no se solapen inicialmente
        HashSet<int> astraZeneca = new HashSet<int>();
        while (astraZeneca.Count < 75)
        {
            int id = rnd.Next(1, 501);
            if (!pfizer.Contains(id)) // Evitar solapamiento inicial
                astraZeneca.Add(id);
        }

        // 3. Operaciones de conjuntos
        // a. Ciudadanos no vacunados: Total - (Pfizer ∪ AstraZeneca)
        HashSet<int> noVacunados = new HashSet<int>(ciudadanos);
        noVacunados.ExceptWith(pfizer);
        noVacunados.ExceptWith(astraZeneca);

        // b. Ciudadanos con ambas dosis: Pfizer ∩ AstraZeneca (en este caso, 0 por diseño)
        HashSet<int> ambasDosis = new HashSet<int>(pfizer);
        ambasDosis.IntersectWith(astraZeneca);

        // c. Solo Pfizer: A - B
        HashSet<int> soloPfizer = new HashSet<int>(pfizer);
        soloPfizer.ExceptWith(astraZeneca);

        // d. Solo AstraZeneca: B - A
        HashSet<int> soloAstraZeneca = new HashSet<int>(astraZeneca);
        soloAstraZeneca.ExceptWith(pfizer);

        // 4. Mostrar resultados
        Console.WriteLine($"Total ciudadanos: {ciudadanos.Count}");
        Console.WriteLine($"No vacunados: {noVacunados.Count}");
        Console.WriteLine($"Ambas dosis: {ambasDosis.Count}");
        Console.WriteLine($"Solo Pfizer: {soloPfizer.Count}");
        Console.WriteLine($"Solo AstraZeneca: {soloAstraZeneca.Count}");

        // Opcional: Mostrar algunos IDs de cada grupo
        Console.WriteLine("\nEjemplo de IDs no vacunados (10 primeros):");
        Console.WriteLine(string.Join(", ", noVacunados.Take(10)));
    }
}