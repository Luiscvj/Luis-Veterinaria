# skeleton-app-webapi


#Cambios

*Realice un cambio en la tabla citas quitando la propiedad Hora y dejando solo fecha ya que DateTime me trae ambas.

*Quite la tabla DetalleMovimiento.Condense su utilidad en tres tablas: Medicamento,MovimientoMedicamento,TipoMovimiento.Aca se da una relacion muchos a muchos entre Medicamento y TipoMovimiento dando como resultado la tabla intermedia MovimientoMedicamento.

*En la tabla TratamientosMedicos agregue un campo que se llama  tipoUnidad que se refiere a gm,ul,ml.Medidas usuales en los medicamentos

*En la tabla Mascotas quite la relacion de especie , ya que esta tambien tiene relacion con raza y raza ya esta en mascota ,por lo cual seria una relacion redundante.




///////////////////CONSULTAS//////////////////////////////

1.     public async Task<dynamic>  TraerMascotasPorEspecieConsulta1()
     {
        
                          

                      return  _context.Mascotas
                                    .GroupBy(m => m.Razas.Especies.NombreEspecie)
                                    .ToDictionary
                                    (
                                        group => group.Key
                                        

                                    ) ;
                 
                           
     }

     Es la primera consulta => http://localhost:5258/Mascota/TraerMascotasPorEspecie_Consulta1



2. public async Task<IEnumerable<Medicamento>> MedicamentosLaboratorio_Consulta2()
        {
            return await _context.Medicamentos.Where(l => l.LaboratorioId == 1).ToListAsync();
        }

    Es la segunda consulta => http://localhost:5258/Medicamento/MedicamentosLaboratorio_Consulta2





3.     public async Task<dynamic> ListarMascotasAtendidasPorVeterinario_Consulta3(string NombreVeterinario)
    {
        var VeterinarioBuscado = await _context.Veterinarios.FirstOrDefaultAsync( x => x.VeterinarioNombre.ToLower() == NombreVeterinario.ToLower());
        if (VeterinarioBuscado == null) return "Veterinario no encontrado";



     return    _context.Citas.Where(x => x.VeterinarioId == VeterinarioBuscado.Id).Include(x => x.Mascotas)
                                .Select(e => new 
                                {
                                    VeterinarioBuscado = VeterinarioBuscado.VeterinarioNombre,
                                    MascotaNombre = e.Mascotas.NombreMascota
                                });
    
    }


    Es la consulta  => http://localhost:5258/Mascota/ListarMascotasAtendidasPorVeterinario_Consulta3?NombreVeterinario=Jorge
