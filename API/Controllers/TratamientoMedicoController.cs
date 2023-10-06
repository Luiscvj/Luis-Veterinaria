using API.Dtos.TratamientoMedicoDTOS;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class TratamientoMedicoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TratamientoMedicoController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(TratamientoMedicoDto TratamientoMedicoDto)
        {
            if(TratamientoMedicoDto == null)
                return BadRequest();

             TratamientoMedico TratamientoMedico = _mapper.Map<TratamientoMedico>(TratamientoMedicoDto);
             _unitOfWork.TratamientosMedicos.Add(TratamientoMedico);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = TratamientoMedico.Id},TratamientoMedico));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<TratamientoMedicoDto> TratamientoMedicoDto)
        {
            if(TratamientoMedicoDto == null)
                return BadRequest();
            
            IEnumerable<TratamientoMedico> TratamientosMedicos = _mapper.Map<IEnumerable<TratamientoMedico>>(TratamientoMedicoDto);

            _unitOfWork.TratamientosMedicos.AddRange(TratamientosMedicos);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(TratamientoMedico TratamientoMedico in TratamientosMedicos)
            {
                CreatedAtAction(nameof(AddRange),new{id = TratamientoMedico.Id}, TratamientoMedico);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TratamientoMedicoDto>> GetById(int id)
        {
            TratamientoMedico TratamientoMedico =  await _unitOfWork.TratamientosMedicos.GetByIdAsync(id);
            if(TratamientoMedico == null)
                return BadRequest();
            return  _mapper.Map<TratamientoMedicoDto>(TratamientoMedico);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<TratamientoMedicoDto>>> GetAll()
        {
            IEnumerable<TratamientoMedico> TratamientosMedicos = await   _unitOfWork.TratamientosMedicos.GetAllAsync();
            if(TratamientosMedicos == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<TratamientoMedicoDto>>(TratamientosMedicos));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]TratamientoMedicoDto TratamientoMedicoDto)
        {
             if(TratamientoMedicoDto == null )
                    return BadRequest();

            TratamientoMedico TratamientoMedico = await _unitOfWork.TratamientosMedicos.GetByIdAsync(id);

            _mapper.Map(TratamientoMedicoDto,TratamientoMedico);//Me mapea cada propiedad de mi TratamientoMedicoDto a la entidad TratamientoMedico
            _unitOfWork.TratamientosMedicos.Update(TratamientoMedico);

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
            TratamientoMedico TratamientoMedico = await _unitOfWork.TratamientosMedicos.GetByIdAsync(id);

            if(TratamientoMedico == null)
                return BadRequest();

            _unitOfWork.TratamientosMedicos.Remove(TratamientoMedico);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

