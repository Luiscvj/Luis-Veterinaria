using API.Dtos.MedicamentoDTOS;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class MedicamentoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicamentoController(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }   
        
        [HttpPost]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult> Add(MedicamentoDto MedicamentoDto)
        {
            if(MedicamentoDto == null)
                return BadRequest();

             Medicamento Medicamento = _mapper.Map<Medicamento>(MedicamentoDto);
             _unitOfWork.Medicamentos.Add(Medicamento);

             int num = await _unitOfWork.SaveAsync();

             if(num == 0)
                return BadRequest();

            

            return Ok(CreatedAtAction(nameof(Add), new {id = Medicamento.Id},Medicamento));
        }

        [HttpPost("AddRange")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult>  AddRange(IEnumerable<MedicamentoDto> MedicamentoDto)
        {
            if(MedicamentoDto == null)
                return BadRequest();
            
            IEnumerable<Medicamento> Medicamentos = _mapper.Map<IEnumerable<Medicamento>>(MedicamentoDto);

            _unitOfWork.Medicamentos.AddRange(Medicamentos);
            int num =await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            foreach(Medicamento Medicamento in Medicamentos)
            {
                CreatedAtAction(nameof(AddRange),new{id = Medicamento.Id}, Medicamento);
            }

            return Ok("Registros  creados con exito");
        }


        [HttpGet("{id}")]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<MedicamentoDto>> GetById(int id)
        {
            Medicamento Medicamento =  await _unitOfWork.Medicamentos.GetByIdAsync(id);
            if(Medicamento == null)
                return BadRequest();
            return  _mapper.Map<MedicamentoDto>(Medicamento);
        }

        [HttpGet("GetAll")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetAll()
        {
            IEnumerable<Medicamento> Medicamentos = await   _unitOfWork.Medicamentos.GetAllAsync();
            if(Medicamentos == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<MedicamentoDto>>(Medicamentos));
        }
        

        
        [HttpGet("MedicamentosLaboratorio_Consulta2")]
       // [Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> MedicamentosLaboratorio_Consulta2()
        {
            IEnumerable<Medicamento> Medicamentos = await   _unitOfWork.Medicamentos.MedicamentosLaboratorio_Consulta2();
            if(Medicamentos == null)
                return BadRequest();
            return Ok(_mapper.Map<IEnumerable<MedicamentoDto>>(Medicamentos));
        }
        
        [HttpPut]
        //[Authorize(Roles="")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Update(int id, [FromBody]MedicamentoDto MedicamentoDto)
        {
             if(MedicamentoDto == null )
                    return BadRequest();

            Medicamento Medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);

            _mapper.Map(MedicamentoDto,Medicamento);//Me mapea cada propiedad de mi MedicamentoDto a la entidad Medicamento
            _unitOfWork.Medicamentos.Update(Medicamento);

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
            Medicamento Medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);

            if(Medicamento == null)
                return BadRequest();

            _unitOfWork.Medicamentos.Remove(Medicamento);

            int num = await _unitOfWork.SaveAsync();
            if(num == 0)
                return BadRequest();

            return Ok("Registro Borrado Con exito");
        }
        
        
        
    }

