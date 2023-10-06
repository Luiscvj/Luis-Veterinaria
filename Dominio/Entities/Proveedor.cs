namespace Dominio.Entities;

public class Proveedor : BaseEntity
{
    public string NombreProveedor { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public List<Medicamento> Medicamentos { get; set; }

}