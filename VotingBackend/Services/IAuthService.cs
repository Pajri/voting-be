using System;
using System.Threading.Tasks;
using VotingBackend.Dtos;

namespace VotingBackend.Services
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> Login(LoginRequestDto login);
        public Task Logout(Guid userId);
    }
}
