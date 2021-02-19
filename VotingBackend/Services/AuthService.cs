using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Helper;
using VotingBackend.Repositories;

namespace VotingBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;


        public AuthService(IUserRepository userRepository, IAuthHelper authHelper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _authHelper = authHelper;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto login)
        {
            var user = await _userRepository.GetUserByEmailAndRole(login.Email, login.Role);
            if(user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            bool verified = _authHelper.VerifyPassword(login.Password, user.Password);
            if (!verified)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var accessToken = _authHelper.GenerateToken(user, _configuration.GetValue<string>("JWTAccessKey"), TokenType.ACCESS_TOKEN);
            var refrehToken = _authHelper.GenerateToken(user, _configuration.GetValue<string>("JWTRefreshKey"), TokenType.ACCESS_TOKEN);

            LoginResponseDto response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refrehToken,
                User = new UserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Role = user.Role
                }
            };

            return response;
        }
    }
}
