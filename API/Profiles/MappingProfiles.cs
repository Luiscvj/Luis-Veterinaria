using API.Dtos.CitaDTOS;
using API.Dtos.EspecieDTOS;
using API.Dtos.LaboratorioDTOS;
using API.Dtos.MascotaDTOS;
using API.Dtos.MedicamentoDTOS;
using API.Dtos.MedicamentoProveedorDTOS;
using API.Dtos.MovimientoMedicamentoDTOS;
using API.Dtos.PropietarioDTOS;
using API.Dtos.ProveedorDTOS;
using API.Dtos.RazaDTOS;
using API.Dtos.RolDTOS;
using API.Dtos.TipoMovimientoDTOS;
using API.Dtos.TratamientoMedicoDTOS;
using API.Dtos.VeterinarioDTOS;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;


public class MappingProfiles  : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Usuario,UsuaroDto>().ReverseMap(); 
        CreateMap<Rol,RolDto>().ReverseMap();
        CreateMap<Cita,CitaDto>().ReverseMap();
        CreateMap<Especie,EspecieDto>().ReverseMap();
        CreateMap<Laboratorio,LaboratorioDto>().ReverseMap();
        CreateMap<Mascota,MascotaDto>().ReverseMap();
        CreateMap<Medicamento,MedicamentoDto>().ReverseMap();
        CreateMap<MedicamentoProveedor,MedicamentoProveedorDto>().ReverseMap();
        CreateMap<MovimientoMedicamento,MovimientoMedicamentoDto>().ReverseMap();
        CreateMap<Propietario,PropietarioDto>().ReverseMap();
        CreateMap<Raza,RazaDto>().ReverseMap();
        CreateMap<Proveedor,ProveedorDto>().ReverseMap();
        CreateMap<TipoMovimiento,TipoMovimientoDto>().ReverseMap();
        CreateMap<TratamientoMedico,TratamientoMedicoDto>().ReverseMap();
        CreateMap<Veterinario,VeterinarioDto>().ReverseMap();
    }
}