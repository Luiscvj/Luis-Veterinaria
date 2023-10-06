using API.Dtos.MascotaDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class MascotaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MascotaController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(MascotaDto MascotaDto)
        {
            if(MascotaDto == null)
                return BadRequest();

             Mascota Mascota = _mapper.Map<Mascota>(MascotaDto);
             _unitOfWork.Mascotas.Add(Mascota);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Mascota.Id},Mascota));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<MascotaDto> MascotaDto)
        {
            if(MascotaDto == null)
                return BadRequest();
            
            IEnumerable<Mascota> Mascotas = _mapper.Map<IEnumerable<Mascota>>(MascotaDto);

            _unitOfWork.Mascotas.AddRange(Mascotas);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Mascota Mascota in Mascotas)
            {
                CreatedAtAction(nameof(AddRange),new{id = Mascota.Id}, Mascota);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MascotaDto>> GetById(int id)
        {
            Mascota Mascota =  await _unitOfWork.Mascotas.GetByIdAsync(id);
            if(Mascota == null)
                return BadRequest();
            return  _mapper.Map<MascotaDto>(Mascota);
        }


        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<MascotaDto>>> MascotaPaginacion([FromQuery] Params hamb_ingParams)
        {
            var Mascotas = await _unitOfWork.Mascotas.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize,hamb_ingParams.Search);
            var ListMascotas=_mapper.Map<List<MascotaDto>>(Mascotas.registros);

            return new Pager<MascotaDto>(ListMascotas,Mascotas.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MascotaDto>>> GetAll()
        {
            IEnumerable<Mascota> Mascotas = await   _unitOfWork.Mascotas.GetAllAsync();
            if(Mascotas == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<MascotaDto>>(Mascotas));
        }



        [HttpGet("TraerMascotasPorEspecie_Consulta1")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<dynamic>>> TraerMascotasPorEspecie_Consulta1()
        {
            var Mascotas = await   _unitOfWork.Mascotas.TraerMascotasPorEspecieConsulta1();
            if(Mascotas == null)
                return BadRequest();
            return Ok(Mascotas);
        }



        [HttpGet("ListarMascotas_PropietariosConGoldenRetriever_Consulta5")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<dynamic>>>  ListarMascotas_PropietariosConGoldenRetriever_Consulta5()
        {
            var Mascotas = await   _unitOfWork.Mascotas.ListarMascotas_PropietariosConGoldenRetriever_Consulta5();
            if(Mascotas == null)
                return BadRequest();
            return Ok(Mascotas);
        }


        [HttpGet("ListarNumeroDeMascotasPorRaza_Consulta6")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<dynamic>>>  ListarNumeroDeMascotasPorRaza_Consulta6()
        {
            var Mascotas = await   _unitOfWork.Mascotas.ListarNumeroDeMascotasPorRaza();
            if(Mascotas == null)
                return BadRequest();
            return Ok(Mascotas);
        }

        [HttpGet("ListarMascotasAtendidasPorVeterinario_Consulta3")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<dynamic>>> ListarMascotasAtendidasPorVeterinario_Consulta3(string NombreVeterinario)
        {
            var Mascotas = await   _unitOfWork.Mascotas.ListarMascotasAtendidasPorVeterinario_Consulta3(NombreVeterinario);
            if(Mascotas == null)
                return BadRequest();
            return Ok(Mascotas);
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]MascotaDto MascotaDto)
        {
             if(MascotaDto == null )
                    return BadRequest();

            Mascota Mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);

            _mapper.Map(MascotaDto,Mascota);//Me mapea cada propiedad de mi MascotaDto a la entidad Mascota
            _unitOfWork.Mascotas.Update(Mascota);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Actualizado con exito");
             
        }


        [HttpDelete]
        [Authorize(Roles="Empleado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        
        public async Task<ActionResult> Delete(int id)
        {
            Mascota Mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);

            if(Mascota == null)
                return BadRequest();

            _unitOfWork.Mascotas.Remove(Mascota);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

