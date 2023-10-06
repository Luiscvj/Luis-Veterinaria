namespace Dominio.Entities;

public class Laboratorio :BaseEntity
{
    public string  LaboratorioNombre { get; set; }
    public string LaboratorioDireccion { get; set; }
    public string LaboratiorTelefono { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
}