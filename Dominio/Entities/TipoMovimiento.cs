namespace Dominio.Entities;

public class TipoMovimiento :BaseEntity
{
    public string DescripccionMovimiento { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public List<MovimientoMedicamento> MovimientosMedicamentos { get; set; }
}