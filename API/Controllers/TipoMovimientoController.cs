using API.Dtos.TipoMovimientoDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class TipoMovimientoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoMovimientoController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(TipoMovimientoDto TipoMovimientoDto)
        {
            if(TipoMovimientoDto == null)
                return BadRequest();

             TipoMovimiento TipoMovimiento = _mapper.Map<TipoMovimiento>(TipoMovimientoDto);
             _unitOfWork.TipoMovimientos.Add(TipoMovimiento);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = TipoMovimiento.Id},TipoMovimiento));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<TipoMovimientoDto> TipoMovimientoDto)
        {
            if(TipoMovimientoDto == null)
                return BadRequest();
            
            IEnumerable<TipoMovimiento> TipoMovimientos = _mapper.Map<IEnumerable<TipoMovimiento>>(TipoMovimientoDto);

            _unitOfWork.TipoMovimientos.AddRange(TipoMovimientos);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(TipoMovimiento TipoMovimiento in TipoMovimientos)
            {
                CreatedAtAction(nameof(AddRange),new{id = TipoMovimiento.Id}, TipoMovimiento);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<TipoMovimientoDto>> GetById(int id)
        {
            TipoMovimiento TipoMovimiento =  await _unitOfWork.TipoMovimientos.GetByIdAsync(id);
            if(TipoMovimiento == null)
                return BadRequest();
            return  _mapper.Map<TipoMovimientoDto>(TipoMovimiento);
        }



        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<TipoMovimientoDto>>> TipoMovimientoPaginacion([FromQuery] Params hamb_ingParams)
        {
            var TipoMovimientos = await _unitOfWork.TipoMovimientos.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize,hamb_ingParams.Search);
            var ListTipoMovimientos=_mapper.Map<List<TipoMovimientoDto>>(TipoMovimientos.registros);

            return new Pager<TipoMovimientoDto>(ListTipoMovimientos,TipoMovimientos.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<TipoMovimientoDto>>> GetAll()
        {
            IEnumerable<TipoMovimiento> TipoMovimientos = await   _unitOfWork.TipoMovimientos.GetAllAsync();
            if(TipoMovimientos == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<TipoMovimientoDto>>(TipoMovimientos));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]TipoMovimientoDto TipoMovimientoDto)
        {
             if(TipoMovimientoDto == null )
                    return BadRequest();

            TipoMovimiento TipoMovimiento = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);

            _mapper.Map(TipoMovimientoDto,TipoMovimiento);//Me mapea cada propiedad de mi TipoMovimientoDto a la entidad TipoMovimiento
            _unitOfWork.TipoMovimientos.Update(TipoMovimiento);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Actualizado con exito");
             
        }


        [HttpDelete]
        [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        
        public async Task<ActionResult> Delete(int id)
        {
            TipoMovimiento TipoMovimiento = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);

            if(TipoMovimiento == null)
                return BadRequest();

            _unitOfWork.TipoMovimientos.Remove(TipoMovimiento);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

