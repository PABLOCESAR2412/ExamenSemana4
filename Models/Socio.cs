namespace ptorresExamen.Models;

public class Socio
{
    public string Usuario { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public int Edad { get; set; }
    public DateTime FechaInicio { get; set; }
    public string Plan { get; set; } = "";
    public bool Casillero { get; set; }
    public decimal MontoInicial { get; set; }
    public decimal PagoMensual { get; set; }
    public decimal PagoTotal { get; set; }
}
