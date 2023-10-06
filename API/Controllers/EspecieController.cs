using API.Dtos.EspecieDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class EspecieController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EspecieController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(EspecieDto EspecieDto)
        {
            if(EspecieDto == null)
                return BadRequest();

             Especie Especie = _mapper.Map<Especie>(EspecieDto);
             _unitOfWork.Especies.Add(Especie);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Especie.Id},Especie));
        }

        [HttpPost("AddRange")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<EspecieDto> EspecieDto)
        {
            if(EspecieDto == null)
                return BadRequest();
            
            IEnumerable<Especie> Especies = _mapper.Map<IEnumerable<Especie>>(EspecieDto);

            _unitOfWork.Especies.AddRange(Especies);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Especie Especie in Especies)
            {
                CreatedAtAction(nameof(AddRange),new{id = Especie.Id}, Especie);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<EspecieDto>> GetById(int id)
        {
            Especie Especie =  await _unitOfWork.Especies.GetByIdAsync(id);
            if(Especie == null)
                return BadRequest();
            return  _mapper.Map<EspecieDto>(Especie);
        }



        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<EspecieDto>>> EspeciePaginacion([FromQuery] Params hamb_ingParams)
        {
            var Especies = await _unitOfWork.Especies.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize,hamb_ingParams.Search);
            var ListEspecies=_mapper.Map<List<EspecieDto>>(Especies.registros);

            return new Pager<EspecieDto>(ListEspecies,Especies.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<EspecieDto>>> GetAll()
        {
            IEnumerable<Especie> Especies = await   _unitOfWork.Especies.GetAllAsync();
            if(Especies == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<EspecieDto>>(Especies));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]EspecieDto EspecieDto)
        {
             if(EspecieDto == null )
                    return BadRequest();

            Especie Especie = await _unitOfWork.Especies.GetByIdAsync(id);

            _mapper.Map(EspecieDto,Especie);//Me mapea cada propiedad de mi EspecieDto a la entidad Especie
            _unitOfWork.Especies.Update(Especie);

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
            Especie Especie = await _unitOfWork.Especies.GetByIdAsync(id);

            if(Especie == null)
                return BadRequest();

            _unitOfWork.Especies.Remove(Especie);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

