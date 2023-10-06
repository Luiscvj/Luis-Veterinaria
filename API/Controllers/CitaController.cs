using API.Dtos.CitaDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class CitaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CitaController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(CitaDto citaDto)
        {
            if(citaDto == null)
                return BadRequest();

             Cita Cita = _mapper.Map<Cita>(citaDto);
             _unitOfWork.Citas.Add(Cita);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Cita.Id},Cita));
        }

        [HttpPost("AddRange")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<CitaDto> CitaDto)
        {
            if(CitaDto == null)
                return BadRequest();
            
            IEnumerable<Cita> Citas = _mapper.Map<IEnumerable<Cita>>(CitaDto);

            _unitOfWork.Citas.AddRange(Citas);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Cita Cita in Citas)
            {
                CreatedAtAction(nameof(AddRange),new{id = Cita.Id}, Cita);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CitaDto>> GetById(int id)
        {
            Cita Cita =  await _unitOfWork.Citas.GetByIdAsync(id);
            if(Cita == null)
                return BadRequest();
            return  _mapper.Map<CitaDto>(Cita);
        }


    
        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<CitaDto>>> CitaPaginacion([FromQuery] Params hamb_ingParams)
        {
            var Citas = await _unitOfWork.Citas.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize,hamb_ingParams.Search);
            var ListCitas=_mapper.Map<List<CitaDto>>(Citas.registros);

            return new Pager<CitaDto>(ListCitas,Citas.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }
    




        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<CitaDto>>> GetAll()
        {
            IEnumerable<Cita> Citas = await   _unitOfWork.Citas.GetAllAsync();
            if(Citas == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<CitaDto>>(Citas));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]CitaDto CitaDto)
        {
             if(CitaDto == null )
                    return BadRequest();

            Cita Cita = await _unitOfWork.Citas.GetByIdAsync(id);

            _mapper.Map(CitaDto,Cita);//Me mapea cada propiedad de mi CitaDto a la entidad Cita
            _unitOfWork.Citas.Update(Cita);

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
            Cita Cita = await _unitOfWork.Citas.GetByIdAsync(id);

            if(Cita == null)
                return BadRequest();

            _unitOfWork.Citas.Remove(Cita);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

