namespace API.Dtos.TratamientoMedicoDTOS;

public class TratamientoMedicoDto
{
    public int Id { get; set;}
    public decimal Dosis { get; set; }
    public string TipoUnidad { get; set; }
    public DateTime FechaAdministracion { get; set; }
    public string Observacion { get; set; }
    public int MedicamentoId { get; set; }
    public int CitaId { get; set; }
}