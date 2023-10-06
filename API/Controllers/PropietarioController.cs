using API.Dtos.PropietarioDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class PropietarioController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropietarioController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(PropietarioDto PropietarioDto)
        {
            if(PropietarioDto == null)
                return BadRequest();

             Propietario Propietario = _mapper.Map<Propietario>(PropietarioDto);
             _unitOfWork.Propietarios.Add(Propietario);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Propietario.Id},Propietario));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<PropietarioDto> PropietarioDto)
        {
            if(PropietarioDto == null)
                return BadRequest();
            
            IEnumerable<Propietario> Propietarios = _mapper.Map<IEnumerable<Propietario>>(PropietarioDto);

            _unitOfWork.Propietarios.AddRange(Propietarios);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Propietario Propietario in Propietarios)
            {
                CreatedAtAction(nameof(AddRange),new{id = Propietario.Id}, Propietario);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<PropietarioDto>> GetById(int id)
        {
            Propietario Propietario =  await _unitOfWork.Propietarios.GetByIdAsync(id);
            if(Propietario == null)
                return BadRequest();
            return  _mapper.Map<PropietarioDto>(Propietario);
        }




        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<PropietarioDto>>> PropietarioPaginacion([FromQuery] Params hamb_ingParams)
        {
            var Propietarios = await _unitOfWork.Propietarios.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize,hamb_ingParams.Search);
            var ListPropietarios=_mapper.Map<List<PropietarioDto>>(Propietarios.registros);

            return new Pager<PropietarioDto>(ListPropietarios,Propietarios.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<PropietarioDto>>> GetAll()
        {
            IEnumerable<Propietario> Propietarios = await   _unitOfWork.Propietarios.GetAllAsync();
            if(Propietarios == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<PropietarioDto>>(Propietarios));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]PropietarioDto PropietarioDto)
        {
             if(PropietarioDto == null )
                    return BadRequest();

            Propietario Propietario = await _unitOfWork.Propietarios.GetByIdAsync(id);

            _mapper.Map(PropietarioDto,Propietario);//Me mapea cada propiedad de mi PropietarioDto a la entidad Propietario
            _unitOfWork.Propietarios.Update(Propietario);

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
            Propietario Propietario = await _unitOfWork.Propietarios.GetByIdAsync(id);

            if(Propietario == null)
                return BadRequest();

            _unitOfWork.Propietarios.Remove(Propietario);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

