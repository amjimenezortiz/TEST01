using AutoMapper;
using KUNAK.VMS.API.Interfaces;
using KUNAK.VMS.API.Responses;
using KUNAK.VMS.CORE.CustomEntities;
using KUNAK.VMS.CORE.DTOs;
using KUNAK.VMS.CORE.Entities;
using KUNAK.VMS.CORE.Exceptions;
using KUNAK.VMS.CORE.Interfaces;
using KUNAK.VMS.CORE.QueryDTOs;
using KUNAK.VMS.CORE.QueryFilters;
using KUNAK.VMS.INFRASTRUCTURE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KUNAK.VMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUsersHasPermissionService _usersHasPermissionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IValidateUserPermissions _validationUserPermissions;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IUsersHasPermissionService usersHasPermissionService, IMapper mapper, IUriService uriService, IValidateUserPermissions validationUserPermissions, IConfiguration configuration)
        {
            _usersHasPermissionService = usersHasPermissionService;
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            _validationUserPermissions = validationUserPermissions;
            _configuration = configuration;
        }

        /// <summary>
        /// Retorna todos los usuarios sin excepción
        /// Return all users without exception
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>

        [HttpGet("GetAllUsersDetails")]
        public IActionResult GetAllUsersDetails([FromQuery] UserQueryFilter filters)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));

                if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:RA_User"].ToString()))
                {
                    var users = _userService.GetAllUsersDetails(filters);
                    var usersCompanyJoin = _mapper.Map<IEnumerable<UserDetailDTO>>(users);

                    //Front said that they don't need pagination for this CRUD
                    var pagination = new Pagination
                    {
                        Length = users.Length,
                        Size = users.Size,
                        Page = users.Page,
                        LastPage = users.LastPage,
                        StartIndex = users.Count == 0 ? 0 : users[0].IdUser,
                        EndIndex = users.Count == 0 ? 0 : users[users.Count >= users.Size ? users.Size - 1 : users.Count - 1].IdUser,
                        HasNextPage = users.HasNextPage,
                        HasPreviousPage = users.HasPreviousPage,
                        NextPageUrl = _uriService.GetUserPaginationUri(filters, Url.RouteUrl(nameof(GetAllUsersDetails))).ToString()
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

                    var response = new ApiResponse<IEnumerable<UserDetailDTO>>(usersCompanyJoin)
                    {
                        Pagination = pagination
                    };
                    return Ok(response);
                }
                else if (_validationUserPermissions.RolePermissionValidation(token, _configuration["Permissions:R_User"].ToString())
                    || _validationUserPermissions.UserPermissionValidation(token, _configuration["Permissions:R_User"].ToString()))
                {
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var users = _userService.GetUsersByCompany(filters, idCompany);
                    var usersDto = _mapper.Map<IEnumerable<UserDetailDTO>>(users);

                    //Front said that they don't need pagination for this CRUD
                    var pagination = new Pagination
                    {
                        Length = users.Length,
                        Size = users.Size,
                        Page = users.Page,
                        LastPage = users.LastPage,
                        StartIndex = users.Count == 0 ? 0 : users[0].IdUser,
                        EndIndex = users.Count == 0 ? 0 : users[users.Count >= users.Size ? users.Size - 1 : users.Count - 1].IdUser,
                        HasNextPage = users.HasNextPage,
                        HasPreviousPage = users.HasPreviousPage,
                        NextPageUrl = _uriService.GetUserPaginationUri(filters, Url.RouteUrl(nameof(GetAllUsersDetails))).ToString()
                    };
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

                    var response = new ApiResponse<IEnumerable<UserDetailDTO>>(usersDto)
                    {
                        Pagination = pagination
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest("No tiene permiso para realizar esta operación");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// <summary>
        /// Retorna los usuarios dependiendo el idCompany que se recibe como parámetro
        /// Returns the users depending on the idCompany that is received as a parameter
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>

        //[HttpGet("GetUsersByCompany")]
        //public IActionResult GetUsersByCompany([FromQuery] UserQueryFilter filters)
        //{
        //    var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
        //    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
        //    var users = _userService.GetUsersByCompany(filters, idCompany);
        //    var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);

        //    //Front said that they don't need pagination for this CRUD
        //    var pagination = new Pagination
        //    {
        //        Length = users.Length,
        //        Size = users.Size,
        //        Page = users.Page,
        //        LastPage = users.LastPage,
        //        StartIndex = users.Count == 0 ? 0 : users[0].IdUser,
        //        EndIndex = users.Count == 0 ? 0 : users[users.Count >= users.Size ? users.Size - 1 : users.Count - 1].IdUser,
        //        HasNextPage = users.HasNextPage,
        //        HasPreviousPage = users.HasPreviousPage,
        //        NextPageUrl = _uriService.GetUserPaginationUri(filters, Url.RouteUrl(nameof(GetUsersByCompany))).ToString()
        //    };
        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

        //    var response = new ApiResponse<IEnumerable<UserDTO>>(usersDto)
        //    {
        //        Pagination = pagination
        //    };
        //    return Ok(response);
        //}

        /////////////////////////////// CRUD BASIC OPERATION FOR THIS ENTITY ///////////////////////////////////////////

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var users = await _userService.GetUser(id);
            var usersDto = _mapper.Map<UserDetailDTO>(users);
            return Ok(usersDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(UserDTO userDTO)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var idRol = token.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                if (idRol == "1")
                {
                    var user = _mapper.Map<User>(userDTO);
                    await _userService.InsertUser(user);
                    var response = _mapper.Map<UserDetailDTO>(user);
                    return Ok(response);
                }
                else
                {
                    //int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    var user = _mapper.Map<User>(userDTO);
                    //user.IdCompany = idCompany;
                    await _userService.InsertUser(user);

                    var response = _mapper.Map<UserDetailDTO>(user);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserPutDTO userPutDTO)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:U_User"].ToString());

                if (validation)
                {
                    var email = token.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                    var user = _mapper.Map<User>(userPutDTO);
                    user.IdUser = id;
                    await _userService.UpdateUser(user, email);
                    User userEdit = _userService.GetUserDetailById(id);
                    var response = _mapper.Map<UserDetailDTO>(userEdit);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("No tiene permiso para realizar esta operación");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(UserResetDTO userResetDTO)
        {
            try
            {
                var currentPassword = _userService.GetSHA256(userResetDTO.CurrentPassword);
                var user = _userService.GetByIdUser(userResetDTO.IdUser);
                if (user.Password == currentPassword)
                {
                    var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                    int idCompany = int.Parse(token.Claims.FirstOrDefault(x => x.Type == "Company").Value);
                    user.Password = userResetDTO.Password;
                    await _userService.ResetPassword(user);
                    return Ok();
                }
                else
                {
                    return BadRequest("La contraseña actual no es válida");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(Request.Headers["Authorization"].ToString().Remove(0, 7));
                var validation = _validationUserPermissions.Validation(token, _configuration["Permissions:D_User"].ToString());

                if (validation)
                {
                    var result = await _userService.DeleteUser(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No tiene permiso para realizar esta operación");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }

}
