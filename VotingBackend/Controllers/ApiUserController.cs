using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Exceptions;
using VotingBackend.Models;
using VotingBackend.Services;

namespace VotingBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly IUserService _service;
        public ApiUserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<User> GetUserById(Guid id)
        {
            return null;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterResponseDto>> RegisterUser(RegisterRequestDto user)
        {
            RegisterResponseDto createdUser = new RegisterResponseDto();

            try
            {
                createdUser = await _service.RegisterUser(user);
                
            }
            catch (Exception ex)
            {
                if (ex is UserAlreadyRegisteredException)
                {
                    return Conflict(ex);
                }

                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            return CreatedAtAction(nameof(GetUserById), createdUser);
        }
    }
}
