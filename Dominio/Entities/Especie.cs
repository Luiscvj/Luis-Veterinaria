namespace Dominio.Entities;

public class Especie : BaseEntity
{
    public string NombreEspecie { get; set; }

    public List<Raza> Razas { get; set; }
}