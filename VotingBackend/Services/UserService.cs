using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Dtos;
using VotingBackend.Exceptions;
using VotingBackend.Helper;
using VotingBackend.Models;
using VotingBackend.Repositories;

namespace VotingBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthHelper _authHelper;

        public UserService(IUserRepository repository, IAuthHelper authHelper)
        {
            _repository = repository;
            _authHelper = authHelper;
        }
        public async Task<RegisterResponseDto> RegisterUser(RegisterRequestDto user)
        {
            try
            {
                //check if user already exists
                var regUser = await _repository.GetUserByEmail(user.Email);
                if (regUser != null)
                {
                    throw new UserAlreadyRegisteredException();
                }

                User newUser = new User()
                {
                    ID = new Guid(),
                    Age = user.Age,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Password = _authHelper.HashPassword(user.Password),
                    Role = user.Role
                };

                var registeredUser = await _repository.AddAsync(newUser);

                RegisterResponseDto userResponseDto = new RegisterResponseDto
                {
                    FirstName = registeredUser.FirstName,
                    LastName = registeredUser.LastName,
                    Age = registeredUser.Age,
                    Email = registeredUser.Email,
                    Gender = registeredUser.Gender
                };

                return userResponseDto;
            }
            catch (Exception ex)
            {
                //TODO handle error
                throw ex;
            }
        }
    }
}
