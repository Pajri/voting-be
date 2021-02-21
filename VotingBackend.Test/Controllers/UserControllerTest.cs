using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using VotingBackend.Controllers;
using VotingBackend.Dtos;
using VotingBackend.Exceptions;
using VotingBackend.Services;
using Xunit;

namespace VotingBackend.Test.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task RegisterUser_Success()
        {
            //arrange
            RegisterRequestDto req = new RegisterRequestDto
            {
                Age = 12,
                Email = "test@example.com",
                FirstName = "test",
                Gender = 1,
                LastName = "test last",
                Password = "Password123",
                PasswordConfirmation = "Password123",
                Role = "Client"
            };

            var mockService = new Mock<IUserService>();
            mockService
                .Setup(service => service.RegisterUser(req))
                .ReturnsAsync(RegisterTest(req));

            var controller = new ApiUserController(mockService.Object);

            //act
            var result = await controller.RegisterUser(req);

            //assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            var model = Assert.IsAssignableFrom<RegisterResponseDto>(actionResult.Value);
            Assert.Equal(12, model.Age);
            Assert.Equal("test@example.com", model.Email);
            Assert.Equal("test", model.FirstName);
            Assert.Equal(1, model.Gender);
            Assert.Equal("test last", model.LastName);
        }

        [Fact]
        public async Task RegisterUser_Conflict()
        {
            //arrange
            RegisterRequestDto req = new RegisterRequestDto();

            var mockService = new Mock<IUserService>();
            mockService
                .Setup(service => service.RegisterUser(req))
                .Throws<UserAlreadyRegisteredException>();

            var controller = new ApiUserController(mockService.Object);

            //act
            var result = await controller.RegisterUser(req);

            //assert
            var actionResult = Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        public async Task RegisterUser_ValidationError()
        {
            //arrange
            RegisterRequestDto req = new RegisterRequestDto();

            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(req, new ValidationContext(req), validationResults, true);

            //assert
            Assert.Equal("The firstName field is required.", validationResults[0].ErrorMessage);
            Assert.Equal("The email field is required.", validationResults[1].ErrorMessage);
            Assert.Equal("The password field is required.", validationResults[2].ErrorMessage);
        }

        private RegisterResponseDto RegisterTest(RegisterRequestDto req)
        {
            return new RegisterResponseDto
            {
                Age = 12,
                Email = "test@example.com",
                FirstName = "test",
                Gender = 1,
                LastName = "test last"
            };
        }
    }
}
