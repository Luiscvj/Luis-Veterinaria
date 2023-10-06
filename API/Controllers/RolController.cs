using API.Dtos.RolDTOS;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
  [ApiVersion("1.0")]
  [ApiVersion("1.1")]
      public class RolController : BaseApiController
      {
          private readonly IUnitOfWork _unitOfWork;
          private readonly IMapper _mapper;
  
          public RolController(IUnitOfWork unitOfWork, IMapper mapper) 
          {
              _mapper = mapper;
              _unitOfWork = unitOfWork;
          }
        [HttpGet]
        [Authorize(Roles="Administrador")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<RolDto>>> GetAll()
        {
            IEnumerable<Rol> Roles = await _unitOfWork.Roles.GetAllAsync();
            var rolDtos= _mapper.Map<IEnumerable<RolDto>>(Roles);
            return Ok(rolDtos);
        }



    [HttpGet("GetRolPag")]
    [MapToApiVersion("1.1")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pager<RolDto>>> GetPaisDep([FromQuery] Params RolParmas )
    {

      

         var Rol = await _unitOfWork.Roles.GetAllAsync(RolParmas.PageIndex,RolParmas.PageSize,RolParmas.Search);
         var listRolesDto =_mapper.Map<List<RolDto>>(Rol.registros);

        return new Pager<RolDto>(listRolesDto,RolParmas.PageSize , Rol.totalRegistros, RolParmas.PageIndex,RolParmas.Search );

        
    }



      }



  
  
  
  