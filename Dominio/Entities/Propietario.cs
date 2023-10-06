namespace Dominio.Entities;


public class Propietario :BaseEntity
{
    public string NombrePropietario { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public List<Mascota> Mascotas { get; set; }
}