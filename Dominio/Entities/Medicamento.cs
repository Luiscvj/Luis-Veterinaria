namespace Dominio.Entities;

public class Medicamento :BaseEntity
{
    public string NombreMedicamento { get; set; }
    public int CantidadDisponible    { get; set; }
    public decimal PrecioMedicamento { get; set; }
    public int LaboratorioId { get; set; }
    public Laboratorio Laboratorios { get; set; }
    public List<TratamientoMedico> TratamientosMedicos { get; set; }
    public List<TipoMovimiento> TiposMovimientos { get; set; }
    public List<MovimientoMedicamento> MovimientosMedicamentos { get; set; }
    public List<Proveedor> Proveedores { get; set; }

}