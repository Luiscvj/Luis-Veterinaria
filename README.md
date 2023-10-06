# skeleton-app-webapi
NOTA: realice Data Seeding , por favor ejecutar la migracion para ver

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


4.   public async Task<List<String>>  ListarProveedoresPorMedicamentoDeterminado_Consulta4(string NombreMedicamento)
     {       
           List<string> Proveedores = new List<string>();
           
            var MedicamentoBuscado =await  _context.Medicamentos.FirstOrDefaultAsync( x => x.NombreMedicamento.ToLower() == NombreMedicamento.ToLower());


           var MedicamentoProveedor= await  _context.MedicamentosProveedores.Where(x => x.MedicamentoId == MedicamentoBuscado.Id).ToListAsync();


            foreach(var e in MedicamentoProveedor)
            {

                Proveedor  p = await   _context.Proveedores.FirstOrDefaultAsync(x => x.Id == e.ProveedorId);
                Proveedores.Add(p.NombreProveedor);
           
            }
        

           return Proveedores; 

          

           
     }


    Es la consulta => http://localhost:5258/Proveedor/ListarProveedoresPorMedicamentoDeterminado_Consulta4?NombreMedicamento=Anti%20Fungico

5. public async  Task<dynamic> ListarMascotas_PropietariosConGoldenRetriever_Consulta5()
    {

           return await  _context.Mascotas.Where(x => x.RazaId ==1).Include(x => x.Propietarios)
                                .Select(e => new 
                                {
                                    NombreMascota = e.NombreMascota,
                                    Propietario = e.Propietarios.NombrePropietario
                                }).ToListAsync();

                        
    }

    
    Es la consulta => http://localhost:5258/Mascota/ListarMascotas_PropietariosConGoldenRetriever_Consulta5


6. 
    public async Task<dynamic> ListarNumeroDeMascotasPorRaza()
    {
        return await _context.Mascotas
                     .GroupBy(x=>x.Razas.RazaNombre)
                     .ToDictionaryAsync
                     (
                        group => group.Key,
                        group => group.Count( )
                     );
                  

    }

    Es la consulta => http://localhost:5258/Mascota/ListarNumeroDeMascotasPorRaza_Consulta6





///////////////////AUTORIZACION Y JWT/////////////////////////

Realice Autorizacion unicamente para los metodos delete de cada controlador.
NOTA:Por favor autenticarse con el controladore de usuario para poder ver la funcionalidad de JWT.




/////////////////PAGINACION//////////////////////////////

Realice paginacion de todos los controladores, con Query y version .Todos se pueden ver con version 1.1  y estan en los GET sin nombre por ejemplo:
http://localhost:5258/Veterinario?v=1.1