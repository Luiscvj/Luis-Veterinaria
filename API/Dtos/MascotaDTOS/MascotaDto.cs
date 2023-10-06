namespace API.Dtos.MascotaDTOS;

public class MascotaDto
{   
    public int Id { get; set; }
    public string NombreMascota { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int PropietarioId { get; set; }
    public int RazaId { get; set; }
}