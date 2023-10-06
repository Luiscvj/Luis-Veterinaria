using API.Dtos.RazaDTOS;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class RazaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RazaController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(RazaDto RazaDto)
        {
            if(RazaDto == null)
                return BadRequest();

             Raza Raza = _mapper.Map<Raza>(RazaDto);
             _unitOfWork.Razas.Add(Raza);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Raza.Id},Raza));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<RazaDto> RazaDto)
        {
            if(RazaDto == null)
                return BadRequest();
            
            IEnumerable<Raza> Razas = _mapper.Map<IEnumerable<Raza>>(RazaDto);

            _unitOfWork.Razas.AddRange(Razas);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Raza Raza in Razas)
            {
                CreatedAtAction(nameof(AddRange),new{id = Raza.Id}, Raza);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<RazaDto>> GetById(int id)
        {
            Raza Raza =  await _unitOfWork.Razas.GetByIdAsync(id);
            if(Raza == null)
                return BadRequest();
            return  _mapper.Map<RazaDto>(Raza);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<RazaDto>>> GetAll()
        {
            IEnumerable<Raza> Razas = await   _unitOfWork.Razas.GetAllAsync();
            if(Razas == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<RazaDto>>(Razas));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]RazaDto RazaDto)
        {
             if(RazaDto == null )
                    return BadRequest();

            Raza Raza = await _unitOfWork.Razas.GetByIdAsync(id);

            _mapper.Map(RazaDto,Raza);//Me mapea cada propiedad de mi RazaDto a la entidad Raza
            _unitOfWork.Razas.Update(Raza);

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
            Raza Raza = await _unitOfWork.Razas.GetByIdAsync(id);

            if(Raza == null)
                return BadRequest();

            _unitOfWork.Razas.Remove(Raza);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

