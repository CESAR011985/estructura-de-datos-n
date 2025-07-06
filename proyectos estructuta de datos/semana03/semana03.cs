using System;

public class Estudiante
{
    // Propiedades  
    public int Id { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Direccion { get; set; }
    public string[] Telefonos { get; set; }  // Array para 3 teléfonos  

    // Constructor  
    public Estudiante(int id, string nombres, string apellidos, string direccion, string[] telefonos)
    {
        Id = id;
        Nombres = nombres;
        Apellidos = apellidos;
        Direccion = direccion;
        Telefonos = telefonos;  // Asignación del array  
    }

    // Método para mostrar datos  
    public void MostrarDatos()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Nombre: {Nombres} {Apellidos}");
        Console.WriteLine($"Dirección: {Direccion}");
        Console.WriteLine("Teléfonos:");
        for (int i = 0; i < Telefonos.Length; i++)
        {
            Console.WriteLine($"  Teléfono {i + 1}: {Telefonos[i]}");
        }
    }
}  

partial class Program  // <-- Añadido 'partial' aquí
{
    static void Main(string[] args)
    {
        // Crear un estudiante  
        string[] telefonosJulio = { "0991222367", "022345678", "0987654321" };
        Estudiante julio = new Estudiante(
            id: 1,
            nombres: "Julio César",
            apellidos: "Trujillo Gonzalez",
            direccion: "otavalo calle jacinto collaguaso y luis alberto de la torre 110",
            telefonos: telefonosJulio
        );

        // Mostrar datos  
        julio.MostrarDatos();

        // Registrar otro estudiante  
        string[] telefonosMaria = { "0975643210", "022987654", "0998765432" };
        Estudiante maria = new Estudiante(2, "Margarita", "Lomas", "otavalo Calle quiroga y 31 de octubre 812", telefonosMaria);
        maria.MostrarDatos();
    }
}
