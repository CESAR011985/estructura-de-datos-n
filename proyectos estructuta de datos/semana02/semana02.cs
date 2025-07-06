using System;

public class Cuadrado
{
    // Propiedad privada para encapsular el lado
    private double _lado;

    // Constructor que recibe el lado al crear el objeto
    public Cuadrado(double lado)
    {
        // Validamos que el lado sea positivo
        if (lado <= 0)
            throw new ArgumentException("El lado debe ser un valor positivo");
        
        _lado = lado;
    }

    // Propiedad pública para acceder al lado con validación
    public double Lado
    {
        get { return _lado; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("El lado debe ser un valor positivo");
            _lado = value;
        }
    }

    // Método para calcular el área del cuadrado
    // Fórmula: lado × lado
    public double CalcularArea()
    {
        return _lado * _lado;
    }

    // Método para calcular el perímetro del cuadrado
    // Fórmula: 4 × lado
    public double CalcularPerimetro()
    {
        return 4 * _lado;
    }

    // Método para mostrar información del cuadrado
    public void MostrarInformacion()
    {
        Console.WriteLine($"Cuadrado - Lado: {_lado}");
        Console.WriteLine($"Área: {CalcularArea():F2}");
        Console.WriteLine($"Perímetro: {CalcularPerimetro():F2}\n");
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Ejemplo de uso de la clase Cuadrado
            Cuadrado miCuadrado = new Cuadrado(5.0);
            miCuadrado.MostrarInformacion();

            // Modificamos el lado y volvemos a mostrar
            miCuadrado.Lado = 3.5;
            Console.WriteLine("Después de modificar el lado:");
            miCuadrado.MostrarInformacion();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}