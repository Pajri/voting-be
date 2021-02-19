using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Models;

namespace VotingBackend.Services
{
    public interface IUserService
    {
        Task<RegisterResponseDto> RegisterUser(RegisterRequestDto user);
    }
}
