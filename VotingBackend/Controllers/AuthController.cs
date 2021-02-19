using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Exceptions;
using VotingBackend.Services;

namespace VotingBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto loginDto)
        {
            LoginResponseDto response;
            try
            {
                response = await _service.Login(loginDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                if(ex is UnauthorizedAccessException)
                {
                    return Unauthorized(ex);
                }

                return StatusCode((int) HttpStatusCode.InternalServerError);
            }

            return Ok(response);
        }
    }
}
