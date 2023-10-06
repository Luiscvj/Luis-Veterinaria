using API.Dtos.VeterinarioDTOS;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class VeterinarioController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VeterinarioController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(VeterinarioDto VeterinarioDto)
        {
            if(VeterinarioDto == null)
                return BadRequest();

             Veterinario Veterinario = _mapper.Map<Veterinario>(VeterinarioDto);
             _unitOfWork.Veterinarios.Add(Veterinario);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Veterinario.Id},Veterinario));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<VeterinarioDto> VeterinarioDto)
        {
            if(VeterinarioDto == null)
                return BadRequest();
            
            IEnumerable<Veterinario> Veterinarios = _mapper.Map<IEnumerable<Veterinario>>(VeterinarioDto);

            _unitOfWork.Veterinarios.AddRange(Veterinarios);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Veterinario Veterinario in Veterinarios)
            {
                CreatedAtAction(nameof(AddRange),new{id = Veterinario.Id}, Veterinario);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<VeterinarioDto>> GetById(int id)
        {
            Veterinario Veterinario =  await _unitOfWork.Veterinarios.GetByIdAsync(id);
            if(Veterinario == null)
                return BadRequest();
            return  _mapper.Map<VeterinarioDto>(Veterinario);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<VeterinarioDto>>> GetAll()
        {
            IEnumerable<Veterinario> Veterinarios = await   _unitOfWork.Veterinarios.GetAllAsync();
            if(Veterinarios == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<VeterinarioDto>>(Veterinarios));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]VeterinarioDto VeterinarioDto)
        {
             if(VeterinarioDto == null )
                    return BadRequest();

            Veterinario Veterinario = await _unitOfWork.Veterinarios.GetByIdAsync(id);

            _mapper.Map(VeterinarioDto,Veterinario);//Me mapea cada propiedad de mi VeterinarioDto a la entidad Veterinario
            _unitOfWork.Veterinarios.Update(Veterinario);

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
            Veterinario Veterinario = await _unitOfWork.Veterinarios.GetByIdAsync(id);

            if(Veterinario == null)
                return BadRequest();

            _unitOfWork.Veterinarios.Remove(Veterinario);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

