namespace API.Dtos.MedicamentoDTOS;

public class MedicamentoDto
{
    public int Id { get; set; }
    public string NombreMedicamento { get; set; }
    public int CantidadDisponible    { get; set; }
    public decimal PrecioMedicamento { get; set; }
    public int LaboratorioId { get; set; }
}