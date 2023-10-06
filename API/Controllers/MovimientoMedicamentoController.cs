using API.Dtos.MovimientoMedicamentoDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MovimientoMedicamentoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovimientoMedicamentoController(IUnitOfWork unitOfWork, IMapper mapper) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(MovimientoMedicamentoDto MovimientoMedicamentoDto)
        {
            if(MovimientoMedicamentoDto == null)
                    return BadRequest();

            MovimientoMedicamento MovimientoMedicamento = _mapper.Map<MovimientoMedicamento>(MovimientoMedicamentoDto);
            _unitOfWork.MovimientosMedicamentos.Add(MovimientoMedicamento);  

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            return CreatedAtAction(nameof(Add), new {id = MovimientoMedicamento.MedicamentoId,MovimientoMedicamento.TipoMovimientoId },MovimientoMedicamento);
        }



       


        [HttpPost("Range")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public async Task<ActionResult> AddRange(IEnumerable<MovimientoMedicamentoDto> MovimientoMedicamentosDto)
        {
            if(MovimientoMedicamentosDto == null)
                    return BadRequest();

            IEnumerable<MovimientoMedicamento> MovimientoMedicamentoes = _mapper.Map<IEnumerable<MovimientoMedicamento>>(MovimientoMedicamentosDto);
            _unitOfWork.MovimientosMedicamentos.AddRange(MovimientoMedicamentoes);  

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            foreach(var c in MovimientoMedicamentoes )
            {
                CreatedAtAction(nameof(AddRange), new {id = c.MedicamentoId,c.TipoMovimientoId},c);
            }

            return Ok();

            
            
        }


        [HttpGet("{HamburgesaId},{IngredienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MovimientoMedicamentoDto>> GetById(int MedicamentoId,int TipoMovimientoId)
        {
            MovimientoMedicamento MovimientoMedicamento =await  _unitOfWork.MovimientosMedicamentos.GetByIdAsync(MedicamentoId,TipoMovimientoId);

                if(MovimientoMedicamento == null)
                    return BadRequest();

            return _mapper.Map<MovimientoMedicamentoDto>(MovimientoMedicamento);

        }

        
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MovimientoMedicamentoDto>>> GetAll()
        {
            IEnumerable<MovimientoMedicamento> MovimientoMedicamento =await  _unitOfWork.MovimientosMedicamentos.GetAllAsync();

                if(MovimientoMedicamento == null)
                    return BadRequest();

            return Ok(_mapper.Map<IEnumerable<MovimientoMedicamentoDto>>(MovimientoMedicamento));

        }
        

        [HttpGet("GetAllHamburguesaPaginacion")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<MovimientoMedicamentoDto>>> GetHamburguesaPaginacion([FromQuery] Params hamb_ingParams)
        {
            var Hamburguesa = await _unitOfWork.MovimientosMedicamentos.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize);
            var listMovimientoMedicamentosDto=_mapper.Map<List<MovimientoMedicamentoDto>>(Hamburguesa.registros);

            return new Pager<MovimientoMedicamentoDto>(listMovimientoMedicamentosDto,Hamburguesa.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        
        public async Task<ActionResult> Update(int MedicamentoId,int TipoMovimientoId , [FromBody]MovimientoMedicamentoDto MovimientoMedicamentoDto)
        {
            if(MovimientoMedicamentoDto == null)
                return BadRequest();

            MovimientoMedicamento MovimientoMedicamento = await _unitOfWork.MovimientosMedicamentos.GetByIdAsync(MedicamentoId,TipoMovimientoId);
            _unitOfWork.MovimientosMedicamentos.Remove(MovimientoMedicamento);
            await _unitOfWork.SaveAsync();
            _mapper.Map(MovimientoMedicamentoDto, MovimientoMedicamento);
           
            _unitOfWork.MovimientosMedicamentos.Add(MovimientoMedicamento);

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            return Ok("REGISTRO ACTUALIZADO CON EXITO");
        }


        [HttpDelete("{HamburguesaId},{IngredienteId}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public async Task<ActionResult> Delete(int MedicamentoId,int TipoMovimientoId)
        {
            MovimientoMedicamento MovimientoMedicamento = await _unitOfWork.MovimientosMedicamentos.GetByIdAsync(MedicamentoId,TipoMovimientoId);

            if(MovimientoMedicamento == null)
                return BadRequest();

            _unitOfWork.MovimientosMedicamentos.Remove(MovimientoMedicamento);

            int num = await _unitOfWork.SaveAsync();

            if (num == 0)
                return BadRequest();

            return NoContent();
        }
    }   
}