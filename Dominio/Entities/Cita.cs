namespace Dominio.Entities;

public class Cita :BaseEntity
{
    public DateTime FechaCita { get; set; }
    public string Motivo { get; set; }
    public int MascotaId { get; set; }
    public Mascota Mascotas { get; set; }
    public int VeterinarioId { get; set; }
    public Veterinario Veterinarios { get; set; }
    public List<TratamientoMedico> TratamientosMedicos { get; set; }
}