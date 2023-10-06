using API.Dtos.ProveedorDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class ProveedorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(ProveedorDto ProveedorDto)
        {
            if(ProveedorDto == null)
                return BadRequest();

             Proveedor Proveedor = _mapper.Map<Proveedor>(ProveedorDto);
             _unitOfWork.Proveedores.Add(Proveedor);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Proveedor.Id},Proveedor));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<ProveedorDto> ProveedorDto)
        {
            if(ProveedorDto == null)
                return BadRequest();
            
            IEnumerable<Proveedor> Proveedores = _mapper.Map<IEnumerable<Proveedor>>(ProveedorDto);

            _unitOfWork.Proveedores.AddRange(Proveedores);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Proveedor Proveedor in Proveedores)
            {
                CreatedAtAction(nameof(AddRange),new{id = Proveedor.Id}, Proveedor);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ProveedorDto>> GetById(int id)
        {
            Proveedor Proveedor =  await _unitOfWork.Proveedores.GetByIdAsync(id);
            if(Proveedor == null)
                return BadRequest();
            return  _mapper.Map<ProveedorDto>(Proveedor);
        }


        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Pager<ProveedorDto>>> ProveedorPaginacion([FromQuery] Params hamb_ingParams)
        {
            var Proveedores = await _unitOfWork.Proveedores.GetAllAsync(hamb_ingParams.PageIndex,hamb_ingParams.PageSize,hamb_ingParams.Search);
            var ListProveedores=_mapper.Map<List<ProveedorDto>>(Proveedores.registros);

            return new Pager<ProveedorDto>(ListProveedores,Proveedores.totalRegistros,  hamb_ingParams.PageIndex, hamb_ingParams.PageSize,hamb_ingParams.Search);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetAll()
        {
            IEnumerable<Proveedor> Proveedores = await   _unitOfWork.Proveedores.GetAllAsync();
            if(Proveedores == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<ProveedorDto>>(Proveedores));
        }

        [HttpGet("ListarProveedoresPorMedicamentoDeterminado_Consulta4")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<List<String>>> ListarProveedoresPorMedicamentoDeterminado_Consulta4(string NombreMedicamento)
        {
            return await  _unitOfWork.Proveedores.ListarProveedoresPorMedicamentoDeterminado_Consulta4(NombreMedicamento);
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]ProveedorDto ProveedorDto)
        {
             if(ProveedorDto == null )
                    return BadRequest();

            Proveedor Proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);

            _mapper.Map(ProveedorDto,Proveedor);//Me mapea cada propiedad de mi ProveedorDto a la entidad Proveedor
            _unitOfWork.Proveedores.Update(Proveedor);

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
            Proveedor Proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);

            if(Proveedor == null)
                return BadRequest();

            _unitOfWork.Proveedores.Remove(Proveedor);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

