namespace API.Dtos.CitaDTOS;


public  class CitaDto 
{
 public int Id { get; set; }
 public DateTime FechaCita { get; set; }
 public string Motivo { get; set; }
 public int MascotaId { get; set; }
 public int VeterinarioId { get; set; }
}