using API.Dtos.LaboratorioDTOS;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class LaboratorioController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LaboratorioController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(LaboratorioDto LaboratorioDto)
        {
            if(LaboratorioDto == null)
                return BadRequest();

             Laboratorio Laboratorio = _mapper.Map<Laboratorio>(LaboratorioDto);
             _unitOfWork.Laboratorios.Add(Laboratorio);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Laboratorio.Id},Laboratorio));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<LaboratorioDto> LaboratorioDto)
        {
            if(LaboratorioDto == null)
                return BadRequest();
            
            IEnumerable<Laboratorio> Laboratorios = _mapper.Map<IEnumerable<Laboratorio>>(LaboratorioDto);

            _unitOfWork.Laboratorios.AddRange(Laboratorios);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Laboratorio Laboratorio in Laboratorios)
            {
                CreatedAtAction(nameof(AddRange),new{id = Laboratorio.Id}, Laboratorio);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<LaboratorioDto>> GetById(int id)
        {
            Laboratorio Laboratorio =  await _unitOfWork.Laboratorios.GetByIdAsync(id);
            if(Laboratorio == null)
                return BadRequest();
            return  _mapper.Map<LaboratorioDto>(Laboratorio);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<LaboratorioDto>>> GetAll()
        {
            IEnumerable<Laboratorio> Laboratorios = await   _unitOfWork.Laboratorios.GetAllAsync();
            if(Laboratorios == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<LaboratorioDto>>(Laboratorios));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]LaboratorioDto LaboratorioDto)
        {
             if(LaboratorioDto == null )
                    return BadRequest();

            Laboratorio Laboratorio = await _unitOfWork.Laboratorios.GetByIdAsync(id);

            _mapper.Map(LaboratorioDto,Laboratorio);//Me mapea cada propiedad de mi LaboratorioDto a la entidad Laboratorio
            _unitOfWork.Laboratorios.Update(Laboratorio);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Actualizado con exito");
             
        }


        [HttpDelete]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        
        public async Task<ActionResult> Delete(int id)
        {
            Laboratorio Laboratorio = await _unitOfWork.Laboratorios.GetByIdAsync(id);

            if(Laboratorio == null)
                return BadRequest();

            _unitOfWork.Laboratorios.Remove(Laboratorio);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

