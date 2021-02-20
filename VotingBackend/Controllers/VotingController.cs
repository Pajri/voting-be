using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
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
        [Authorize(Roles = Constant.ROLE_ADMIN + "," + Constant.ROLE_CLIENT)]
        public async Task<ActionResult<Voting>> Index([FromQuery] SieveModel sieveModel)
        {
            return Ok(await _service.GetAllVoting(sieveModel)); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VotingDto>> Index(Guid id)
        {
            try
            {
                var votingDetail = await _service.GetVoteDetail(id);
                return Ok(votingDetail);
            } 
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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
