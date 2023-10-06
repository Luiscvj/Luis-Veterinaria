namespace Dominio.Entities;

public class Mascota : BaseEntity
{
    public string NombreMascota { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int PropietarioId { get; set; }
    public Propietario Propietarios { get; set; }

    public int RazaId { get; set; }
    public Raza Razas { get; set; }
    public List<Cita> Citas { get; set; }
}