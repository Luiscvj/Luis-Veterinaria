namespace Dominio.Entities;

public class Veterinario : BaseEntity
{
     public string VeterinarioNombre { get; set; }
     public string VeterinarioEmail { get; set; }
     public string VeterinarioTelefono { get; set; }
     public string Especialidad { get; set; }
     public List<Cita> Citas { get; set; }
}