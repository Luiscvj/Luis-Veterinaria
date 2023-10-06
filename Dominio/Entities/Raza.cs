namespace Dominio.Entities;


public class Raza : BaseEntity
{
    public int EspecieId { get; set; }
    public Especie Especies { get; set; }
    public string RazaNombre { get; set; }
    public List<Mascota> Mascotas { get; set; }
}