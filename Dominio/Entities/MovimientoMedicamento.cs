namespace Dominio.Entities;


public class MovimientoMedicamento 
{
    public int MedicamentoId { get; set; }
    public Medicamento Medicamentos { get; set; }
    public int TipoMovimientoId { get; set; } 
    public TipoMovimiento TiposMovimientos { get; set; }
    public int CantidadMovida { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public decimal PrecioMovimiento { get; set; }
}