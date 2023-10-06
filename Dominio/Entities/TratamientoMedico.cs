namespace Dominio.Entities;

public class TratamientoMedico :BaseEntity
{
    public decimal Dosis { get; set; }
    public string TipoUnidad { get; set; }
    public DateTime FechaAdministracion { get; set; }
    public string Observacion { get; set; }
    public int MedicamentoId { get; set; }
    public Medicamento Medicamentos { get; set; }
    public int CitaId { get; set; }
    public Cita Citas { get; set; }
    
}