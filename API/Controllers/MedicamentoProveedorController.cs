using API.Dtos.MedicamentoProveedorDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MedicamentoProveedorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicamentoProveedorController(IUnitOfWork unitOfWork, IMapper mapper) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(MedicamentoProveedorDto MedicamentoProveedorDto)
        {
            if(MedicamentoProveedorDto == null)
                    return BadRequest();

            MedicamentoProveedor MedicamentoProveedor = _mapper.Map<MedicamentoProveedor>(MedicamentoProveedorDto);
            _unitOfWork.MedicamentosProveedores.Add(MedicamentoProveedor);  

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            return CreatedAtAction(nameof(Add), new {id = MedicamentoProveedor.ProveedorId,MedicamentoProveedor.MedicamentoId },MedicamentoProveedor);
        }



       


        [HttpPost("Range")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

            public async Task<ActionResult> AddRange(IEnumerable<MedicamentoProveedorDto> MedicamentoProveedoresDto)
        {
            if(MedicamentoProveedoresDto == null)
                    return BadRequest();

            IEnumerable<MedicamentoProveedor> MedicamentoProveedores = _mapper.Map<IEnumerable<MedicamentoProveedor>>(MedicamentoProveedoresDto);
            _unitOfWork.MedicamentosProveedores.AddRange(MedicamentoProveedores);  

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            foreach(var c in MedicamentoProveedores )
            {
                CreatedAtAction(nameof(AddRange), new {id = c.MedicamentoId,c.ProveedorId},c);
            }

            return Ok();

            
            
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MedicamentoProveedorDto>> GetById(int MedicamentoId,int ProveedorId)
        {
            MedicamentoProveedor MedicamentoProveedor =await  _unitOfWork.MedicamentosProveedores.GetByIdAsyncProveedorMedicamento(MedicamentoId,ProveedorId);

                if(MedicamentoProveedor == null)
                    return BadRequest();

            return _mapper.Map<MedicamentoProveedorDto>(MedicamentoProveedor);

        }

        
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MedicamentoProveedorDto>>> GetAll()
        {
            IEnumerable<MedicamentoProveedor> MedicamentoProveedor =await  _unitOfWork.MedicamentosProveedores.GetAllAsync();

                if(MedicamentoProveedor == null)
                    return BadRequest();

            return Ok(_mapper.Map<IEnumerable<MedicamentoProveedorDto>>(MedicamentoProveedor));

        }
        

        [HttpGet("Paginacion")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<MedicamentoProveedorDto>>> MedicamentoProveedorPaginacion([FromQuery] Params hamb_ingParams)
        {
            var Hamburguesa = await _unitOfWork.MedicamentosProveedores.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize);
            var listMedicamentoProveedoresDto=_mapper.Map<List<MedicamentoProveedorDto>>(Hamburguesa.registros);

            return new Pager<MedicamentoProveedorDto>(listMedicamentoProveedoresDto,Hamburguesa.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        
        public async Task<ActionResult> Update(int MedicamentoId,int ProveedorId , [FromBody]MedicamentoProveedorDto MedicamentoProveedorDto)
        {
            if(MedicamentoProveedorDto == null)
                return BadRequest();

            MedicamentoProveedor MedicamentoProveedor = await _unitOfWork.MedicamentosProveedores.GetByIdAsyncProveedorMedicamento(MedicamentoId,ProveedorId);
            _unitOfWork.MedicamentosProveedores.Remove(MedicamentoProveedor);
            await _unitOfWork.SaveAsync();
            _mapper.Map(MedicamentoProveedorDto, MedicamentoProveedor);
           
            _unitOfWork.MedicamentosProveedores.Add(MedicamentoProveedor);

            int num = await _unitOfWork.SaveAsync();

            if(num == 0)
                return BadRequest();

            return Ok("REGISTRO ACTUALIZADO CON EXITO");
        }


        [HttpDelete] 
         [Authorize(Roles="Empleado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        

        public async Task<ActionResult> Delete(int MedicamentoId,int ProveedorId)
        {
            MedicamentoProveedor MedicamentoProveedor = await _unitOfWork.MedicamentosProveedores.GetByIdAsyncProveedorMedicamento(MedicamentoId,ProveedorId);

            if(MedicamentoProveedor == null)
                return BadRequest();

            _unitOfWork.MedicamentosProveedores.Remove(MedicamentoProveedor);

            int num = await _unitOfWork.SaveAsync();

            if (num == 0)
                return BadRequest();

            return NoContent();
        }
    }   
}