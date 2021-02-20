using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VotingBackend.Constants;
using VotingBackend.Dtos;
using VotingBackend.Exceptions;
using VotingBackend.Models;
using VotingBackend.Services;

namespace VotingBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/voting")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private readonly IVotingService _service;

        public VotingController(IVotingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Voting>> Index()
        {
            return Ok(await _service.GetAllVoting()); 
        }

        [HttpPost]
        [Authorize(Roles = Constant.ROLE_ADMIN)]
        public async Task<ActionResult<CreateVotingResponseDto>> Create(CreateVotingRequestDto request)
        {
            var voting = await _service.CreateVoting(request);
            return Ok(voting);
        }

        [HttpPost("vote")]
        [Authorize(Roles = Constant.ROLE_CLIENT)]
        public async Task<ActionResult<VotingDto>> Vote(VoteRequestDto request)
        {
            try
            {
                var userId = new Guid(User.Identities.First().Name);
                var voting = await _service.Vote(request, userId);
                return Ok(voting);
            }
            catch (Exception ex)
            {
                if(ex is HttpExceptionBase)
                {
                    return BadRequest(ex);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        
    }
}
