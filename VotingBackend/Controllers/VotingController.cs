using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Constants;
using VotingBackend.Dtos;
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

        
    }
}
