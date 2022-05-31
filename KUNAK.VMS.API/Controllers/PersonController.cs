using AutoMapper;
using KUNAK.VMS.API.Responses;
using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public PersonController(IPersonService personService, IMapper mapper, IUriService uriService)
        {
            _personService = personService;
            _mapper = mapper;
            _uriService = uriService;

        }
        [HttpGet]
        public IActionResult GetPersons([FromQuery] PersonQueryFilter filters)
        {
            try
            {
                //var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                //if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                //{
                var persons = _personService.GetPersons(filters);
                var personsDtos = _mapper.Map<IEnumerable<PersonDTO>>(persons);
                return Ok(personsDtos);
                //}
                //else
                //{
                //    return BadRequest("No tiene permiso para realizar esta operación");
                //}
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var persons = await _personService.GetPerson(id);
            var personsDto = _mapper.Map<PersonDTO>(persons);
            return Ok(personsDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostPerson(PersonDTO personDTO)
        {
            try
            {
                var person = _mapper.Map<Person>(personDTO);
                await _personService.InsertPerson(person);
                var response = _mapper.Map<PersonDTO>(person);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, PersonDTO personDTO)
        {
            try
            {
                var person = _mapper.Map<Person>(personDTO);
                person.IdPerson = id;
                await _personService.UpdatePerson(person);
                var personEdit = await _personService.GetPerson(id);
                var response = _mapper.Map<PersonDTO>(personEdit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _personService.DeletePerson(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
