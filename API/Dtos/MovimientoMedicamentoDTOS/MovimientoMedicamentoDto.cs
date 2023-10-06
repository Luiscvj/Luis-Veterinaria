namespace API.Dtos.MovimientoMedicamentoDTOS;


public class MovimientoMedicamentoDto
{
    public int MedicamentoId { get; set; }
    public int TipoMovimientoId { get; set; } 
    public int CantidadMovida { get; set; }
   public DateTime FechaMovimiento { get; set; }
   public decimal PrecioMovimiento { get; set; }
}