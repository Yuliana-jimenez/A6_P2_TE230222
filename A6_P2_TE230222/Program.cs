using System.Text;

Console.OutputEncoding = Encoding.UTF8;

// Objeto: lote creado a partir de la clase LoteInspeccion
LoteInspeccion lote = new LoteInspeccion(10);

Console.WriteLine("=== Inspección de lote de piezas ===");
Console.WriteLine("Introduzca 1 si la pieza es correcta o 0 si es defectuosa.\n");

// Ciclo for: una captura por pieza (termina tras 10 piezas)
for (int i = 1; i <= lote.TotalPiezas; i++)
{
    int valor;
    // Ciclo while: repite mientras el valor sea distinto de 0 y 1
    while (true)
    {
        Console.Write($"Pieza {i}/{lote.TotalPiezas} (1 = correcta, 0 = defectuosa): ");
        if (int.TryParse(Console.ReadLine()?.Trim(), out valor) && lote.RegistrarPieza(valor))
            break;
        Console.WriteLine("  Valor rechazado: solo se acepta 1 o 0.");
    }
}

lote.MostrarResumen();

// ===== Clase: LoteInspeccion =====
class LoteInspeccion
{
    // Propiedades
    public int TotalPiezas { get; }          // int: piezas del lote
    public int Correctas { get; private set; }    // int: piezas correctas
    public int Defectuosas { get; private set; }  // int: piezas defectuosas
    public double PorcentajeCorrectas =>          // double: % de correctas (protegido contra división entre cero)
        TotalPiezas > 0 ? (double)Correctas / TotalPiezas * 100.0 : 0.0;
    public string Estado =>                       // string: ACEPTADO si >= 90 %
        PorcentajeCorrectas >= 90.0 ? "ACEPTADO" : "RECHAZADO";

    public LoteInspeccion(int totalPiezas) { TotalPiezas = totalPiezas; }

    // Método: registra una pieza (1 = correcta, 0 = defectuosa); devuelve false si el valor es inválido
    public bool RegistrarPieza(int valor)
    {
        if (valor == 1) { Correctas++; return true; }
        if (valor == 0) { Defectuosas++; return true; }
        return false;
    }

    // Método: muestra cantidades, porcentaje y estado
    public void MostrarResumen()
    {
        Console.WriteLine("\n--- Resultados del lote ---");
        Console.WriteLine($"Piezas correctas:   {Correctas} pzas");
        Console.WriteLine($"Piezas defectuosas: {Defectuosas} pzas");
        Console.WriteLine($"Porcentaje correctas: {PorcentajeCorrectas:F1} %");
        Console.WriteLine($"Estado del lote: {Estado}");
        Console.WriteLine("---------------------------------------------------------------------------------");
    }
}